using Microsoft.EntityFrameworkCore;
using NCBASoapAPICountryServices.Context;

namespace NCBASoapAPICountryServices.Middleware
{
    public static class CustomDbContext
    {
        public static void InjectDbContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<CountryDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL"), sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5, // The maximum number of retry attempts
                        maxRetryDelay: TimeSpan.FromSeconds(30), // The maximum delay between retries
                        errorNumbersToAdd: null // Optional list of additional error numbers to add to the list of retryable errors
                    );
                });
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
        }
    }
}
