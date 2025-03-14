using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NCBASoapAPICountryServices.Data.Requests;
using NCBASoapAPICountryServices.Models;
using NCBASoapAPICountryServices.Services.Interfaces;
using System.Globalization;

namespace NCBASoapAPICountryServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly ILogger<CountryController> _logger;

        public CountryController(ICountryService countryService, ILogger<CountryController> logger)
        {
            _countryService = countryService;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetCountryByISOCode")]
        public async Task<IActionResult> GetCountryInfo([FromQuery] CountryIsoCodeRequest request)
        {
            _logger.LogInformation($"Received request to lookup country: {request?.ISoCode}");

            if (string.IsNullOrEmpty(request?.ISoCode))
            {
                _logger.LogWarning("Invalid request: Country name is null or empty");
                return BadRequest("Country name is required");
            }

            try
            {

                var countryInfo = await _countryService.GetCountryInfoByName(request.ISoCode);

                return Ok(countryInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing country lookup for {request.ISoCode}");
                return StatusCode(500, "An error occurred while processing your request");
            }
        }
        [HttpPost]
        [Route("GetCountryByName")]
        public async Task<IActionResult> GetCountryISOCode([FromBody] CountryNameRequest request)
        {
            _logger.LogInformation($"Received request to lookup ISO code for country: {request.Name}");

            if (string.IsNullOrEmpty(request.Name))
            {
                _logger.LogWarning("Invalid request: Country name is null or empty");
                return BadRequest("Country name is required");
            }

            try
            {

                _logger.LogInformation($"Looking up ISO code for country: {request.Name}");
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

                string countryName = textInfo.ToTitleCase(request.Name.ToLower());

                var isoCode = await _countryService.GetCountryISOCode(request.Name);

                return Ok(isoCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing ISO code lookup for {request.Name}");
                return StatusCode(500, "An error occurred while processing your request");
            }
        }
        [HttpGet]
        [Route("GetFullCountryInfo")]
        public async Task<IActionResult> GetFullCountryInfo([FromQuery] string ISoCode)
        {
            _logger.LogInformation($"Received request to lookup full country information for: {ISoCode}");

            if (string.IsNullOrEmpty(ISoCode))
            {
                _logger.LogWarning("Invalid request: Country name is null or empty");
                return BadRequest("Country name is required");
            }

            try
            {

                _logger.LogInformation($"Looking up full country information for: {ISoCode}");
                var countryInfo = await _countryService.GetFullCountryInfo(ISoCode);

                return Ok(countryInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing full country information lookup for {ISoCode}");
                return StatusCode(500, "An error occurred while processing your request");
            }
        }
        [HttpGet]
        [Route("GetCountryById")]
        public async Task<IActionResult> GetCountryInfoById([FromQuery] string ID)
        {
            _logger.LogInformation($"Received request to lookup country information by ID: {ID}");
            if (!string.IsNullOrEmpty(ID))
            {
                try
                {
                    var countryInfo = await _countryService.GetCountryInfoById(Guid.Parse(ID));
                    return Ok(countryInfo);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error processing country information lookup for ID: {ID}");
                    return StatusCode(500, "An error occurred while processing your request");
                }
            }
            else
            {
                _logger.LogWarning("Invalid request: Country ID is null or empty");
                return BadRequest("Country ID is required");
            }
        }
        [HttpPost]
        [Route("UpdateCountryInfo")]
        public async Task<IActionResult> UpdateCountryInfo([FromBody] CountryInfo countryInfo)
        {
            _logger.LogInformation($"Received request to update country information for: {countryInfo?.CountryName}");

            if (countryInfo == null)
            {
                _logger.LogWarning("Invalid request: Country information is null");
                return BadRequest("Country information is required");
            }

            try
            {
                var updatedCountryInfo = await _countryService.UpdateCountryInfo(countryInfo);
                return Ok(updatedCountryInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing country information update for {countryInfo.CountryName}");
                return StatusCode(500, "An error occurred while processing your request");
            }
        }
        [HttpDelete]
        [Route("DeleteCountryInfo")]
        public async Task<IActionResult> DeleteCountryInfo([FromBody] DeleteCountryRequest request)
        {
            _logger.LogInformation($"Received request to delete country information by ID: {request.ID}");
            if (!string.IsNullOrEmpty(request.ID))
            {
                try
                {
                    var countryInfo = await _countryService.DeleteCountryInfo(Guid.Parse(request.ID));
                    return Ok(countryInfo);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error processing country information deletion for ID: {request.ID}");
                    return StatusCode(500, "An error occurred while processing your request");
                }
            }
            else
            {
                _logger.LogWarning("Invalid request: Country ID is null or empty");
                return BadRequest("Country ID is required");
            }
        }
    }  
}
