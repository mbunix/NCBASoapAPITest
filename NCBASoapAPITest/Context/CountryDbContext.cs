
using Microsoft.EntityFrameworkCore;
using NCBASoapAPICountryServices.Models;
namespace NCBASoapAPICountryServices.Context
{
    public class CountryDbContext: DbContext
    {
        public CountryDbContext(DbContextOptions<CountryDbContext> options) : base(options)
        {
        }
        public DbSet<CountryInfo> Countries { get; set; }
    }
}
