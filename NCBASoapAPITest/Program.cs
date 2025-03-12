using NCBASoapAPICountryServices.Middleware;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.InjectDbContext();
builder.Services.AddSingleton<ServiceReference1.CountryInfoServiceSoapType>(
    serviceProvider => {
    var binding = new System.ServiceModel.BasicHttpBinding();
    binding.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.None;
    var endpoint = new System.ServiceModel.EndpointAddress("http://webservices.oorsprong.org/websamples.countryinfo/CountryInfoService.wso");
    return new ServiceReference1.CountryInfoServiceSoapTypeClient(binding, endpoint);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
