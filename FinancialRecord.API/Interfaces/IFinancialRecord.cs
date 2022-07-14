using FinancialRecord.API.ViewModels;

namespace FinancialRecord.API.Interfaces
{
    public interface IFinancialRecord
    {
        public Task<IEnumerable<FinancialRecordResponse>> ReadRecord(FinancialRecordRequest request);
    }
}
