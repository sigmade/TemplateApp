using DataLayer.Products.DataProvider;

namespace WebApi.Extensions
{
    public static class DataProviderExtension
    {
        public static IServiceCollection AppDataProviders(this IServiceCollection services)
        {
            services.AddScoped<IProductDataProvider, ProductDataProvider>();

            return services;
        }
    }
}