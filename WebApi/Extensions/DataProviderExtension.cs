using DataLayer.Products.DataProvider;
using DataLayer.Users.DataProvider;

namespace WebApi.Extensions
{
    public static class DataProviderExtension
    {
        public static IServiceCollection AppDataProviders(this IServiceCollection services)
        {
            services.AddScoped<IProductDataProvider, ProductDataProvider>();
            services.AddScoped<IUserDataProvider, UserDataProvider>();

            return services;
        }
    }
}