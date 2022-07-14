using FinancialRecord.API.Context;
using FinancialRecord.API.Interfaces;
using FinancialRecord.API.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FinancialRecord.API.Services
{
    public class FinancialRecordService : IFinancialRecord
    {
        #region Global Declarations
        private readonly ExperianDBContext _context;
        #endregion

        #region Constructor
        public FinancialRecordService(ExperianDBContext context)
        {
            _context = context;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Fetch the financial records by sending FirstName and Lastname as input search 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IEnumerable<FinancialRecordResponse>> ReadRecord(FinancialRecordRequest request)
        {
            List<FinancialRecordResponse> response = new List<FinancialRecordResponse>();

            try
            {
                var finanicalRecordList = await _context.FinancialInfos.Where(f => f.CustomerInfo.FirstName == request.FirstName && f.CustomerInfo.Surname == request.Surname).Include(p => p.CustomerInfo)
                    .ToListAsync();
                foreach (var record in finanicalRecordList)
                {
                    FinancialRecordResponse fr = new FinancialRecordResponse();
                    fr.CustomerInfo = record.CustomerInfo;
                    fr.FirstName = record.CustomerInfo.FirstName;
                    fr.Surname = record.CustomerInfo.Surname;
                    fr.Dob = record.CustomerInfo.Dob.Date.ToShortDateString();
                    fr.Address = record.CustomerInfo.Address;
                    fr.Postcode = record.CustomerInfo.Postcode;

                    fr.AccountType = record.AccountType.Value.ToString();
                    fr.InitialAmount = record.InitialAmount.Value;
                    fr.InitialTerm = record.InitialTerm.Value;
                    fr.InterestRate = record.InterestRate.Value;

                    fr.MinimumPaymentAmount = record.MinimumPaymentAmount.Value;
                    fr.RemainingAmount = record.RemainingAmount.Value;
                    fr.RemainingTerm = record.RemainingTerm.Value;
                    fr.RepaymentAmount = record.RepaymentAmount.Value;
                    fr.Status = record.Status.Value.ToString();
                    fr.TotalPaymentAmount = record.TotalPaymentAmount.Value;
                    fr.TransactionDate = record.TransactionDate.Date.ToShortDateString();
                    response.Add(fr);
                }
            }
            catch (Exception ex)
            {
                //response.IsSuccess = false;
                //response.Message = ex.Message;
            }

            return response;
        }
        #endregion

    }
}
