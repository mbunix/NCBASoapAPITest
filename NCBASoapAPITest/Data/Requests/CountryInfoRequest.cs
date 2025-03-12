namespace NCBASoapAPICountryServices.Data.Requests
{
    public class CountryIsoCodeRequest
    {
        public string? ISoCode { get; set; }
    }

    public class DeleteCountryRequest
    {
        public string? ID { get; set; }
    }
    public class CountryNameRequest
    {
        public string? Name { get; set; }
    }
}
