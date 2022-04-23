using Library.EventBus.AppConfigs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Library.Shared.DI.Configs
{
    public static class KafkaMessageBrokerDIConfig
    {
        public static IServiceCollection AddKafkaMessageBroker(this IServiceCollection services, IConfiguration configuration)
            => services
                .AddEventBus()
                .Configure<KafkaConfig>(configuration.GetSection(nameof(KafkaConfig)))
                .AddSingleton<KafkaConfig>(s => s.GetRequiredService<IOptions<KafkaConfig>>().Value);
    }
}