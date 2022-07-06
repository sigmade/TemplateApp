using BusinessLayer.Products.Services;
using WebApi.monitoring.Errors;

namespace WebApi.Extensions
{
    public static class AppServicesExtenisons
    {
        public static IServiceCollection AppServices(this IServiceCollection services)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            switch (env)
            {
                case "StubBusinessLayer":
                    services.AddScoped<IProductService, StubProductService>();
                    break;

                default:
                    services.AddScoped<IProductService, ProductService>();
                    break;
            }

            services.AddSingleton<IErrorHandler, ErrorHandler>();

            return services;
        }
    }
}