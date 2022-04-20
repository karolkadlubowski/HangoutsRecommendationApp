using Library.Shared.AppConfigs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Venue.API.Application.Database;
using Venue.API.Application.Database.Repositories;
using Venue.API.Domain.Configuration;
using Venue.API.Infrastructure.Database;
using Venue.API.Infrastructure.Database.Repositories;

namespace Venue.API.DI
{
    public static class DatabaseDIConfig
    {
        public static IServiceCollection AddVenueDbContext(this IServiceCollection services, IConfiguration configuration)
            => services
                .AddDbContext<VenueDbContext>(options
                    => options.UseNpgsql(configuration.Get<ServiceConfiguration>()
                        .DatabaseConfig.DatabaseConnectionString))
                .Configure<DatabaseConfig>(configuration.GetSection(nameof(DatabaseConfig)))
                .AddSingleton<DatabaseConfig>(s => s.GetRequiredService<IOptions<DatabaseConfig>>().Value)
                .AddScoped<IUnitOfWork, UnitOfWork>();

        public static IServiceCollection AddRepositories(this IServiceCollection services)
            => services
                .AddScoped<IVenueRepository, VenueRepository>();
    }
}