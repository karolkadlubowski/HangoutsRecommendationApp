using Identity.API.Application.Database.Repositories;
using Identity.API.Domain.Configuration;
using Identity.API.Infrastructure.Database;
using Identity.API.Infrastructure.Database.Repositories;
using Library.Shared.AppConfigs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Identity.API.DI
{
    public static class DatabaseDIConfig
    {
        public static IServiceCollection AddIdentityDbContext(this IServiceCollection services, IConfiguration configuration)
            => services
                .AddDbContext<IdentityDbContext>(opt
                    => opt.UseNpgsql(configuration.Get<ServiceConfiguration>()
                        .DatabaseConfig.DatabaseConnectionString))
                .Configure<DatabaseConfig>(configuration.GetSection(nameof(DatabaseConfig)))
                .AddSingleton(s => s.GetRequiredService<IOptions<DatabaseConfig>>().Value);

        public static IServiceCollection AddRepositories(this IServiceCollection services)
            => services.AddScoped<IIdentityRepository, IdentityRepository>();
    }
}