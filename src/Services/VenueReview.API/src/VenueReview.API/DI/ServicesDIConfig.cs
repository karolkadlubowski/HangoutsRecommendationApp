using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VenueReview.API.Application.Abstractions;
using VenueReview.API.Application.Services;

namespace VenueReview.API.DI
{
    public static class ServicesDIConfig
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddScoped<IReadOnlyVenueReviewService,VenueReviewService>()
                .AddScoped<IVenueReviewService, VenueReviewService>();
        }
    }
}