using FileStorage.API.Application.Database.Repositories;
using FileStorage.API.Domain.Configuration;
using FileStorage.API.Infrastructure.Database;
using FileStorage.API.Infrastructure.Database.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FileStorage.API.DI
{
    public static class DatabaseDIConfig
    {
        public static IServiceCollection AddFileStorageDbContext(this IServiceCollection services, IConfiguration configuration)
            => services
                .AddTransient<FileStorageDbContext>()
                .Configure<DatabaseConfig>(configuration.GetSection(nameof(DatabaseConfig)))
                .AddSingleton<DatabaseConfig>(s => s.GetRequiredService<IOptions<DatabaseConfig>>().Value);

        public static IServiceCollection AddRepositories(this IServiceCollection services)
            => services
                .AddTransient<IFolderInformationRepository, FolderInformationRepository>();
    }
}