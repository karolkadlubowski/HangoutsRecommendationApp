using FileStorage.API.Application.Abstractions;
using FileStorage.API.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileStorage.API.DI
{
    public static class ServicesDIConfig
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddScoped<IReadOnlyFolderInformationService, FolderInformationService>()
                .AddScoped<IFolderInformationService, FolderInformationService>()
                .AddScoped<IFileInformationService, FileInformationService>();

            return services;
        }
    }
}