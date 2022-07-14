namespace FinancialRecord.API.ViewModels
{
    /// <summary>
    /// Response after uploading the file
    /// </summary>
    public class UploadCSVFileResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
