using FinancialRecord.API.ApplicationEnum;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancialRecord.API.Models
{
    /// <summary>
    /// Financial record to save in database
    /// </summary>
    public class FinancialInfo : BaseEntity
    {
        public virtual CustomerInfo? CustomerInfo { get; set; }
        public FinanceAccountType? AccountType { get; set; }
        public double? InitialAmount { get; set; }
        public double? TotalPaymentAmount { get; set; }
        public double? RepaymentAmount { get; set; }
        public double? RemainingAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public double? MinimumPaymentAmount { get; set; }
        [Column(TypeName = "decimal(4,2)")]
        public decimal? InterestRate { get; set; }
        public double? InitialTerm { get; set; }
        public double? RemainingTerm { get; set; }
        public FinanceStatus? Status { get; set; }
    }
}
