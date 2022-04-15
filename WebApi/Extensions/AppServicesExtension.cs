using BusinessLayer.Products.Services;
using WebApi.monitoring.Errors;

namespace WebApi.Extensions
{
    public static class AppServicesExtenisons
    {
        public static IServiceCollection AppServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ErrorHandler>();

            return services;
        }
    }
}