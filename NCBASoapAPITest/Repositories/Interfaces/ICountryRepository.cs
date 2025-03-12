using NCBASoapAPICountryServices.Models;

namespace NCBASoapAPICountryServices.Repositories.Interfaces
{
    public interface ICountryRepository
    {
        Task<IEnumerable<CountryInfo>> GetAllCountriesAsync();
        Task<CountryInfo> GetCountryByIdAsync(Guid id);
        Task<CountryInfo> SaveCountryInfoAsync(CountryInfo countryInfo);
        Task<CountryInfo> UpdateCountryInfoAsync(CountryInfo countryInfo);
        Task<CountryInfo> DeleteCountryInfoAsync(Guid id);

    }
}
