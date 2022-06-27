using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using VenueReview.API.Application.Database.Repositories;
using VenueReview.API.Domain.Configuration;
using VenueReview.API.Infrastructure.Database;
using VenueReview.API.Infrastructure.Database.Repositories;

namespace VenueReview.API.DI
{
    public static class DatabaseDIConfig
    {
        public static IServiceCollection AddVenueReviewDbContext(this IServiceCollection services, IConfiguration configuration)
            => services
                .AddTransient<VenueReviewDbContext>()
                .Configure<DatabaseConfig>(configuration.GetSection(nameof(DatabaseConfig)))
                .AddSingleton<DatabaseConfig>(s => s.GetRequiredService<IOptions<DatabaseConfig>>().Value);

        public static IServiceCollection AddRepositories(this IServiceCollection services)
            => services
                .AddTransient<IVenueReviewRepository, VenueReviewRepository>();
    }
}