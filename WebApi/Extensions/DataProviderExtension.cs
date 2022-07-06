using DataLayer.Products.DataProviders;

namespace WebApi.Extensions
{
    public static class DataProviderExtension
    {
        public static IServiceCollection AppDataProviders(this IServiceCollection services)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            switch (env)
            {
                case "StubDataLayer":
                    services.AddScoped<IProductDataProvider, StubProductDataProvider>();
                    break;

                default:
                    services.AddScoped<IProductDataProvider, ProductDataProvider>();
                    break;
            }

            return services;
        }
    }
}