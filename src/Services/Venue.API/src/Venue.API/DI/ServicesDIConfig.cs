using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Venue.API.Application.Abstractions;
using Venue.API.Application.Services;

namespace Venue.API.DI
{
    public static class ServicesDIConfig
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddScoped<IReadOnlyVenueService, VenueService>()
                .AddScoped<IVenueService, VenueService>();

            return services;
        }
    }
}