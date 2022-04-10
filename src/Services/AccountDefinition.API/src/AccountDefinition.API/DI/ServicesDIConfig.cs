using AccountDefinition.API.Application.Abstractions;
using AccountDefinition.API.Infrastructure.Services;
using Library.Shared.Resources;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AccountDefinition.API.DI
{
    public static class ServicesDIConfig
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddScoped<IReadOnlyAccountTypeService, AccountTypeService>()
                .AddScoped<IReadOnlyAccountProviderService, AccountProviderService>()
                .AddScoped<IAccountProviderService, AccountProviderService>();

            services
                .AddSingleton<IResourceReader, EmbeddedResourceReader>();

            return services;
        }
    }
}