using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using VenueList.API.Application.Database.Repositories;
using VenueList.API.Domain.Configuration;
using VenueList.API.Infrastructure.Database;
using VenueList.API.Infrastructure.Database.Repositories;

namespace VenueList.API.DI
{
    public static class DatabaseDIConfig
    {
        public static IServiceCollection AddVenueListDbContext(this IServiceCollection services, IConfiguration configuration)
            => services
                .AddTransient<VenueListDbContext>()
                .Configure<DatabaseConfig>(configuration.GetSection(nameof(DatabaseConfig)))
                .AddSingleton<DatabaseConfig>(s => s.GetRequiredService<IOptions<DatabaseConfig>>().Value);

        public static IServiceCollection AddRepositories(this IServiceCollection services)
            => services
                .AddTransient<IFavoriteRepository, FavoriteRepository>();
    }
}