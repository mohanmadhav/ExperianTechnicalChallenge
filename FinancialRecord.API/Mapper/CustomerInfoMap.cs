using CsvHelper.Configuration;
using FinancialRecord.API.Models;

namespace FinancialRecord.API.Mapper
{
    /// <summary>
    /// Mapper class to map the csv header values to domain specific
    /// </summary>
    public class CustomerInfoMap : ClassMap<CustomerInfo>
    {
        public CustomerInfoMap()
        {
            Map(c => c.FirstName).Index(0);
            Map(c => c.Surname).Index(1);
            Map(c => c.Dob).Index(2);
            Map(c => c.Address).Index(3);
            Map(c => c.Postcode).Index(4);
        }
    }
}
