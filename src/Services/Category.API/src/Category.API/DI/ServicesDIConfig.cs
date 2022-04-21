using Category.API.Application.Abstractions;
using Category.API.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Category.API.DI
{
    public static class ServicesDIConfig
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddScoped<IReadOnlyCategoryService, CategoryService>()
                .AddScoped<ICategoryService, CategoryService>();

            return services;
        }
    }
}