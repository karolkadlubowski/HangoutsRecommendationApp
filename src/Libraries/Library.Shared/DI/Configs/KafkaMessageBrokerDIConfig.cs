using Library.EventBus;
using Library.EventBus.Abstractions;
using Library.EventBus.AppConfigs;
using Library.Shared.Events;
using Library.Shared.Events.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Library.Shared.DI.Configs
{
    public static class KafkaMessageBrokerDIConfig
    {
        public static IServiceCollection AddKafkaMessageBroker(this IServiceCollection services, IConfiguration configuration)
            => services
                .Configure<KafkaConfig>(configuration.GetSection(nameof(KafkaConfig)))
                .AddSingleton<KafkaConfig>(s => s.GetRequiredService<IOptions<KafkaConfig>>().Value)
                .AddScoped<IEventPublisher, KafkaEventPublisher>()
                .AddScoped<IEventConsumer, KafkaEventConsumer>()
                .AddScoped<IEventSender, EventSender>();
    }
}