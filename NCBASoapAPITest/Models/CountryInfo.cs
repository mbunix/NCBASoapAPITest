namespace NCBASoapAPICountryServices.Models
{
    public class CountryInfo 
    {
        public Guid Id { get; set; }
        public string? CountryName { get; set; }
        public string? CapitalCity { get; set; }
        public string? CountryFlag { get; set; }
        public string? CountryISOCode { get; set; }
        public string? CurrencyISOCode { get; set; }
        public string? CountryPhoneCode { get; set; }
        public string? ContinentCode { get; set; }
    }
}
