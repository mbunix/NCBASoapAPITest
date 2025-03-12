using NCBASoapAPICountryServices.Models;
using NCBASoapAPICountryServices.Repositories.Interfaces;
using NCBASoapAPICountryServices.Services.Interfaces;
using ServiceReference1;

namespace NCBASoapAPICountryServices.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        public readonly ILogger<CountryService> _logger;
        private readonly CountryInfoServiceSoapTypeClient _client;
        public CountryService(ICountryRepository repository, ILogger<CountryService> logger, CountryInfoServiceSoapTypeClient client)
        {
            _client = client;
            _countryRepository = repository;
            _logger = logger;


        }
        public async  Task<string> GetCountryISOCode(string countryName)
        {
            try
            {
                _logger.LogInformation($"Fetching ISO code for country: {countryName}");
               
                var request = new CountryISOCodeRequest();
                var requestBody = new CountryISOCodeRequestBody { sCountryName = countryName};
                var response = await _client.CountryISOCodeAsync(countryName);

                _logger.LogInformation($"Retrieved ISO code: {response.Body}");
       
                if (response.Body.CountryISOCodeResult == null)
                {
                    _logger.LogWarning($"No ISO code found for country {countryName}");
                    return null;
                }
                var persisted = await _countryRepository.SaveCountryInfoAsync( new CountryInfo
                {
                    CountryISOCode = response.Body.CountryISOCodeResult,
                });
                return persisted.CountryISOCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching ISO code for country {countryName}");
                throw;
            }
        }

        public async Task<CountryInfo> GetCountryInfoByName(string ISOCode)
        {
            try
            {
                _logger.LogInformation($"Fetching country information for: {ISOCode}");

                var request = new CountryNameRequest();


                var requestBody = new CountryNameRequestBody {sCountryISOCode =ISOCode };
                var response = await _client.CountryNameAsync(ISOCode);

                if (response.Body.CountryNameResult == null)
                {
                    _logger.LogWarning($"No information found for country {ISOCode}");
                    return null;
                }

                var countryInfo = response.Body.CountryNameResult;
                var persisted = await _countryRepository.SaveCountryInfoAsync(new CountryInfo
                {
                    CountryName = countryInfo.ToString(),
                });

                return persisted;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetCountryInfoByName");
                return null;
            }
        }

        public async Task<CountryInfo> GetFullCountryInfo(string ISOCode)
        {
            try
            {
                _logger.LogInformation($"Fetching full country information for: {ISOCode}");
                var request = new FullCountryInfoRequest();
                var requestBody = new FullCountryInfoRequestBody { sCountryISOCode = ISOCode };
                var response = await _client.FullCountryInfoAsync(ISOCode);
                _logger.LogInformation($"Retrieved full country information: {response.Body}");
                if (response.Body.FullCountryInfoResult == null)
                {
                    _logger.LogWarning($"No information found for country {ISOCode}");
                    return null;
                }
                var persisted = await _countryRepository.SaveCountryInfoAsync(new CountryInfo
                {
                    CountryName = response.Body.FullCountryInfoResult.sName,
                    CapitalCity = response.Body.FullCountryInfoResult.sCapitalCity,
                    CountryFlag = response.Body.FullCountryInfoResult.sCountryFlag,
                    CountryISOCode = response.Body.FullCountryInfoResult.sISOCode,
                    CurrencyISOCode = response.Body.FullCountryInfoResult.sCurrencyISOCode,
                    CountryPhoneCode = response.Body.FullCountryInfoResult.sPhoneCode,
                    ContinentCode = response.Body.FullCountryInfoResult.sContinentCode
                });
                return persisted;

            }catch(Exception ex)
            {
                _logger.LogError(ex, "Error in GetFullCountryInfo");
                return null;
            }
        }

        public Task<CountryInfo> GetCountryInfoById(Guid ID)
        {
            try
            {
                _logger.LogInformation($"Fetching country information for ID: {ID}");
                var countryInfo = _countryRepository.GetCountryByIdAsync(ID);
                return countryInfo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetCountryInfoById");
                return null;
            }
        }

        public Task<CountryInfo> UpdateCountryInfo(CountryInfo countryInfo)
        {
            try
            {
                _logger.LogInformation($"Updating country information for: {countryInfo.CountryName}");
                var updatedCountryInfo = _countryRepository.UpdateCountryInfoAsync(countryInfo);
                return updatedCountryInfo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateCountryInfo");
                return null;
            }
        }

        public Task<CountryInfo> DeleteCountryInfo(Guid ID)
        {
            try
            {
                _logger.LogInformation($"Deleting country information for ID: {ID}");
                var deletedCountryInfo = _countryRepository.DeleteCountryInfoAsync(ID);
                return deletedCountryInfo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteCountryInfo");
                return null;
            }
        }
    }
}
