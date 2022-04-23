using Library.Shared.DI.Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserProfile.API.Application.Abstractions;
using UserProfile.API.Application.Handlers;
using UserProfile.API.Application.Handlers.Strategies.Factories;

namespace UserProfile.API.DI
{
    public static class ServicesDIConfig
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddKafkaMessageBroker(configuration)
                .AddSingleton<IEventAggregator, EventAggregator>()
                .AddSingleton<IEventHandlerStrategyFactory, EventHandlerStrategyFactory>();

            return services;
        }
    }
}