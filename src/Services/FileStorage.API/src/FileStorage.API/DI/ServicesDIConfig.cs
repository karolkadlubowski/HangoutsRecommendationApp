using FileStorage.API.Application.Abstractions;
using FileStorage.API.Application.Services;
using FileStorage.API.Infrastructure.Adapters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileStorage.API.DI
{
    public static class ServicesDIConfig
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddScoped<IReadOnlyFolderService, FolderService>()
                .AddScoped<IFolderService, FolderService>()
                .AddScoped<IFileService, FileService>();

            services
                .AddSingleton<IFileSystemAdapter, FileSystemAdapter>();

            return services;
        }
    }
}