using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Venue.API.DI
{
    public static class ServicesDIConfig
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}