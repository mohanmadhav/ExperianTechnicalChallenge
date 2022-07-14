using FinancialRecord.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialRecord.API.Context
{
    public class ExperianDBContext : DbContext
    {
        public ExperianDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<CustomerInfo> CustomerInfos { get; set; }
        public DbSet<FinancialInfo> FinancialInfos { get; set; }
    }
}
