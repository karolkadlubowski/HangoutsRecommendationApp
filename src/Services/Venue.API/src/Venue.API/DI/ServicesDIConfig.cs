using Library.Shared.Clients.Factories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Venue.API.Application.Abstractions;
using Venue.API.Application.Services;
using Venue.API.Infrastructure.Clients.Factories;
using Venue.API.Infrastructure.Services;

namespace Venue.API.DI
{
    public static class ServicesDIConfig
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddScoped<IReadOnlyVenueService, VenueService>()
                .AddScoped<IVenueService, VenueService>();

            services
                .AddSingleton<IRestClientFactory, CategoryRestClientFactory>()
                .AddSingleton<ICategoryDataService, CategoryDataService>();

            return services;
        }
    }
}