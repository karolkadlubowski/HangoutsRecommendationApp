using Library.EventBus;
using Library.EventBus.Abstractions;
using Library.Shared.Events;
using Library.Shared.Events.Abstractions;
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
                .AddSingleton<IEventAggregatorCacheCleaner, DefaultEventAggregatorCacheCleaner>();

        public static IServiceCollection AddEventHandlerStrategyFactory<TEventHandlerStrategyFactory>(this IServiceCollection services)
            where TEventHandlerStrategyFactory : class, IEventHandlerStrategyFactory
            => services.AddSingleton<IEventHandlerStrategyFactory, TEventHandlerStrategyFactory>();
    }
}