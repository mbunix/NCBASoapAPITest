using Microsoft.EntityFrameworkCore;
using NCBASoapAPICountryServices.Context;
using NCBASoapAPICountryServices.Models;
using NCBASoapAPICountryServices.Repositories.Interfaces;

namespace NCBASoapAPICountryServices.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly CountryDbContext _context;
        private readonly ILogger<CountryRepository> _logger;
        public CountryRepository( CountryDbContext context, ILogger<CountryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<CountryInfo> DeleteCountryInfoAsync(Guid id)
        {
            _logger.LogInformation("Deleting country info");
            try
            {
                var countryInfo = await _context.Countries.FirstOrDefaultAsync(c => c.Id == id);
                if (countryInfo == null)
                {
                    _logger.LogWarning($"Country with id {id} not found");
                    return null;
                }
                _context.Countries.Remove(countryInfo);
                await _context.SaveChangesAsync();
                return countryInfo;
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error deleting country info");
                return null;

            }
        }

        public async Task<IEnumerable<CountryInfo>> GetAllCountriesAsync()
        {
            _logger.LogInformation("Getting all countries");
            try
            {
                return await _context.Countries.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all countries");
                return null;
            }
        }

        public async Task<CountryInfo> GetCountryByIdAsync(Guid id)
        {
            _logger.LogInformation("Getting country by id");
            try
            {
                return await _context.Countries.FirstOrDefaultAsync(c => c.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting country by id");
                return null;
            }
        }

        public async  Task<CountryInfo> SaveCountryInfoAsync(CountryInfo countryInfo)
        {
               _logger.LogInformation("Saving country info");
            try
            {
                await _context.Countries.AddAsync(countryInfo);
                await _context.SaveChangesAsync();
                return countryInfo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving country info");
                return null;
            }
        }

        public async Task<CountryInfo> UpdateCountryInfoAsync(CountryInfo countryInfo)
        {
            _logger.LogInformation("Updating country info");
            try
            {
                _context.Countries.Update(countryInfo);
                await _context.SaveChangesAsync();
                return countryInfo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating country info");
                return null;
            }
  
        }
    }
}
