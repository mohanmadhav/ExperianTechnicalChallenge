using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations;

namespace FinancialRecord.API.Models
{
    /// <summary>
    /// Customer Domain to save in DB
    /// </summary>
    public class CustomerInfo : BaseEntity
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? Surname { get; set; }
        public DateTime Dob { get; set; }
        public string? Address { get; set; }
        public string? Postcode { get; set; }
    }
}
