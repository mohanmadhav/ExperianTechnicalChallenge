using FinancialRecord.API.ViewModels;

namespace FinancialRecord.API.Interfaces
{
    /// <summary>
    /// Contract to upload the csv file
    /// </summary>
    public interface IUploadFile
    {
        public Task<UploadCSVFileResponse> UploadCSVFile(IFormFile file);
    }
}
