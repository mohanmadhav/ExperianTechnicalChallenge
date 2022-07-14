using CsvHelper.Configuration;
using FinancialRecord.API.Models;

namespace FinancialRecord.API.Mapper
{
    /// <summary>
    /// Mapper class to map the csv header values to domain specific
    /// </summary>
    public class FinancialInfoMap :  ClassMap<FinancialInfo>
    {
        public FinancialInfoMap()
        {
            Map(c => c.CustomerInfo).Index(0);
            Map(c => c.AccountType).Index(4);
            Map(c => c.InitialAmount).Index(5);
            Map(c => c.TotalPaymentAmount).Index(6);
            Map(c => c.RepaymentAmount).Index(7);
            Map(c => c.RemainingAmount).Index(8);
            Map(c => c.TransactionDate).Index(9);
            Map(c => c.MinimumPaymentAmount).Index(10);
            Map(c => c.InterestRate).Index(11);
            Map(c => c.InitialTerm).Index(12);
            Map(c => c.RemainingTerm).Index(13);
            Map(c => c.Status).Index(14);
        }
    }
}
