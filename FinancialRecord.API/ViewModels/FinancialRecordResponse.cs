using FinancialRecord.API.Models;

namespace FinancialRecord.API.ViewModels
{
    public class FinancialRecordResponse
    {
        public CustomerInfo? CustomerInfo { get; set; }
        public string? FirstName { get; set; }
        public string? Surname { get; set; }
        public string? Dob { get; set; }
        public string? Address { get; set; }
        public string? Postcode { get; set; }
        public string? AccountType { get; set; }
        public double? InitialAmount { get; set; }
        public double? TotalPaymentAmount { get; set; }
        public double? RepaymentAmount { get; set; }
        public double? RemainingAmount { get; set; }
        public string? TransactionDate { get; set; }
        public double? MinimumPaymentAmount { get; set; }
        public decimal? InterestRate { get; set; }
        public double? InitialTerm { get; set; }
        public double? RemainingTerm { get; set; }
        public string? Status { get; set; }
    }
}
