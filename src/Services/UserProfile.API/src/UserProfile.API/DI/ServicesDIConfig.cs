using System.Reflection;
using Library.Shared.DI.Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UserProfile.API.DI
{
    public static class ServicesDIConfig
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddKafkaMessageBroker(configuration)
                .AddEventHandlersStrategies(Assembly.Load("UserProfile.API.Application"));

            return services;
        }
    }
}