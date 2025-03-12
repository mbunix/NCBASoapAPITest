using NCBASoapAPICountryServices.Models;

namespace NCBASoapAPICountryServices.Services.Interfaces
{
    public interface ICountryService
    {
        Task<string> GetCountryISOCode(string countryName);
        Task<CountryInfo> GetFullCountryInfo(string isoCode);
        Task<CountryInfo> GetCountryInfoByName(string countryName);
        Task<CountryInfo> GetCountryInfoById(Guid ID);
        Task<CountryInfo> UpdateCountryInfo(CountryInfo countryInfo);
        Task<CountryInfo> DeleteCountryInfo(Guid ID);
    }
}
