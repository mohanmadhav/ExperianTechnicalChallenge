using CsvHelper;
using CsvHelper.Configuration;
using FinancialRecord.API.Context;
using FinancialRecord.API.Helpers;
using FinancialRecord.API.Interfaces;
using FinancialRecord.API.Mapper;
using FinancialRecord.API.Models;
using FinancialRecord.API.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace FinancialRecord.API.Services
{
    public class UploadFileService : IUploadFile
    {
        #region Global Declarations
        private readonly ExperianDBContext _context;
        public bool inputDateValidFormat = false;
        public int SuccessRecords = 0;
        public int FailedRecords = 0;
        #endregion

        #region Constructor
        public UploadFileService(ExperianDBContext context)
        {
            this._context = context;
        }
        #endregion
        #region public Methods
        /// <summary>
        /// Read and Parse the all csv files and save into the database
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<UploadCSVFileResponse> UploadCSVFile(IFormFile file)
        {
            UploadCSVFileResponse response = new UploadCSVFileResponse();
            
            response.IsSuccess = true;

            try
            {
                //Save the csv file to the root folder
                var folderName = Path.Combine("Resources", "UploadedFiles");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var fileExtension = Path.GetExtension(file.FileName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    if (fileExtension == ".csv")
                    {
                        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
                        {
                            Delimiter = ",",
                            HasHeaderRecord = false,
                            MissingFieldFound = null,
                        };

                        using (var reader = new StreamReader(fullPath, System.Text.Encoding.UTF8))

                        using (var csv = new CsvReader(reader, configuration))
                        {
                            csv.Read();
                            csv.Context.RegisterClassMap<CustomerInfoMap>();
                            csv.Context.RegisterClassMap<FinancialInfoMap>();

                            var customerInfoRecords = await _context.CustomerInfos.ToListAsync();
                            var financialRecords = await _context.FinancialInfos.ToListAsync();

                            if (customerInfoRecords.Count == 0)
                            {
                                while (csv.Read())
                                {
                                    string parseRecord = csv.Parser.RawRecord;
                                    string[] fields = Regex.Split(parseRecord, ",(?=(?:[^\']*\'[^\']*\')*(?![^\']*\'))");

                                    //Invoke CustomerInfo csv file to upload the data
                                    await UploadCustomerInfoFile(fields);
                                }
                            }
                            else if (financialRecords.Count == 0 || customerInfoRecords.Count == 0)
                            {
                                while (csv.Read())
                                {
                                    string parseRecord = csv.Parser.RawRecord;
                                    string[] financialInfoFields = Regex.Split(parseRecord, ",(?=(?:[^\']*\'[^\']*\')*(?![^\']*\'))");
                                    await UploadFinancialInfoFile(financialInfoFields);
                                }
                            }
                            else
                            {
                                response.Message = $"You have already uploaded {fileName} file, please try to add another file to proceed further";
                                response.IsSuccess = false;
                                return response;
                            }
                            response.Message = $"Successfully Uploaded {fileName}. Total Records are {SuccessRecords + FailedRecords}, out of which {SuccessRecords} are uploaded and saved in database and {FailedRecords} are failed to upload. ";
                        }
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "InValid File";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }
        #endregion

        #region private helper methods
        /// <summary>
        /// Read and Parse the Financial record csv and insert the data into database considering based on the customer info
        /// </summary>
        /// <param name="financialInfoFields">financialInfoFields</param>
        /// <returns></returns>
        private async Task UploadFinancialInfoFile(string[] financialInfoFields)
        {
            //Get the CustomerInfo Id based on firstname and surname to update the financial records
            var custInfoId = await _context.CustomerInfos.Where(x => x.FirstName == financialInfoFields[0].ToString() && x.Surname == financialInfoFields[1].ToString()).Select(i => i.Id).FirstOrDefaultAsync();

            FinancialInfo financialInfo = new FinancialInfo();
            financialInfo.CustomerInfo = await _context.CustomerInfos.FirstOrDefaultAsync(c => c.Id == custInfoId);
            financialInfo.AccountType = UploadFileHelper.GetFinanceAccountTypeEnumValue(financialInfoFields[4].ToString());
            financialInfo.InitialAmount = UploadFileHelper.ConvertToDouble(financialInfoFields[5].ToString());
            financialInfo.TotalPaymentAmount = UploadFileHelper.ConvertToDouble(financialInfoFields[6].ToString());
            financialInfo.RepaymentAmount = UploadFileHelper.ConvertToDouble(financialInfoFields[7].ToString());
            financialInfo.RemainingAmount = UploadFileHelper.ConvertToDouble(financialInfoFields[8].ToString());

            if (financialInfoFields[9] != null)
            {
                //Check valid Date format
                inputDateValidFormat = DateTimeHelper.IsValidDate(financialInfoFields[9].ToString());
                if (inputDateValidFormat)
                {
                    financialInfo.TransactionDate = DateTime.ParseExact(financialInfoFields[9], "dd/MM/yyyy", new CultureInfo("en-GB"));
                }
                else
                {
                    financialInfo.TransactionDate = DateTime.Today;
                }
            }
            else
            {
                financialInfo.TransactionDate = DateTime.Today;
            }

            financialInfo.MinimumPaymentAmount = UploadFileHelper.ConvertToDouble(financialInfoFields[10].ToString());
            financialInfo.InterestRate = UploadFileHelper.ConvertToDecimal(financialInfoFields[11].ToString());

            financialInfo.InitialTerm = UploadFileHelper.ConvertToDouble(financialInfoFields[12].ToString());
            financialInfo.RemainingTerm = UploadFileHelper.ConvertToDouble(financialInfoFields[13].ToString());
            financialInfo.Status = UploadFileHelper.GetFinanceStatusEnumValue(financialInfoFields[14].ToString());

            if (!string.IsNullOrEmpty(financialInfoFields[0].ToString()) && !string.IsNullOrEmpty(financialInfoFields[1].ToString()) &&
                financialInfo.CustomerInfo != null)
            {
                _context.FinancialInfos.Add(financialInfo);
                await _context.SaveChangesAsync();
                SuccessRecords++;
            }
            else
            {
                //ToDo : Capture the failed messages with reason
                FailedRecords++;
            }
        }
        /// <summary>
        /// Read and Parse the csv name file to insert the customer info into the database
        /// </summary>
        /// <param name="customerInfoFields">customerInfoFields</param>
        /// <returns></returns>
        private async Task UploadCustomerInfoFile(string[] customerInfoFields)
        {
            CustomerInfo customerInfo = new CustomerInfo();
            customerInfo.FirstName = customerInfoFields[0].ToString() ?? string.Empty;
            customerInfo.Surname = customerInfoFields[1].ToString() ?? string.Empty;
            if (customerInfoFields[2] != null)
            {
                //Check valid Date format
                inputDateValidFormat = DateTimeHelper.IsValidDate(customerInfoFields[2].ToString());
                if (inputDateValidFormat)
                {
                    customerInfo.Dob = DateTime.ParseExact(customerInfoFields[2], "dd/MM/yyyy", new CultureInfo("en-GB"));
                }
                else
                {
                    customerInfo.Dob = DateTime.Today;
                }
            }
            else
            {
                customerInfo.Dob = DateTime.Today;
            }
            customerInfo.Address = customerInfoFields[3].ToString().Replace("'", "") ?? string.Empty;
            customerInfo.Postcode = customerInfoFields[4].ToString().Replace("'", "") ?? string.Empty;

            if (!string.IsNullOrEmpty(customerInfo.FirstName) && !string.IsNullOrEmpty(customerInfo.Surname)
                && inputDateValidFormat)
            {
                _context.CustomerInfos.Add(customerInfo);
                await _context.SaveChangesAsync();
                SuccessRecords++;
            }
            else
            {
                //ToDo : Capture the failed messages with reason
                FailedRecords++;
            }
        }

        #endregion
    }
}
