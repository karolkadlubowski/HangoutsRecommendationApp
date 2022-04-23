using System.Reflection;
using Library.EventBus;
using Library.EventBus.Abstractions;
using Library.Shared.Events;
using Library.Shared.Events.Abstractions;
using Library.Shared.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Shared.DI.Configs
{
    public static class EventBusDIConfig
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services)
            => services
                .AddScoped<IEventPublisher, KafkaEventPublisher>()
                .AddScoped<IEventSender, EventSender>()
                .AddSingleton<IEventConsumer, KafkaEventConsumer>()
                .AddSingleton<IEventAggregator, EventAggregator>()
                .AddSingleton<IEventAggregatorCacheCleaner, DefaultEventAggregatorCacheCleaner>()
                .AddEventHandlerStrategyFactory<EventHandlerStrategyFactory>();

        public static IServiceCollection AddEventHandlersStrategies(this IServiceCollection services, Assembly assembly)
            => services.RegisterAllTypes<IEventHandlerStrategy>(new[] { assembly },
                lifetime: ServiceLifetime.Singleton);

        public static IServiceCollection AddEventHandlerStrategyFactory<TEventHandlerStrategyFactory>(this IServiceCollection services)
            where TEventHandlerStrategyFactory : class, IEventHandlerStrategyFactory
            => services.AddSingleton<IEventHandlerStrategyFactory, TEventHandlerStrategyFactory>();
    }
}