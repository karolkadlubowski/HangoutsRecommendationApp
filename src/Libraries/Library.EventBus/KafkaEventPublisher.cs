using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Library.EventBus.Abstractions;
using Library.EventBus.AppConfigs;

namespace Library.EventBus
{
    public class KafkaEventPublisher : IEventPublisher
    {
        private readonly IProducer<EventType, string> _producer;

        public KafkaEventPublisher(KafkaSettings kafkaSettings)
        {
            var config = new ProducerConfig { BootstrapServers = kafkaSettings.BootstrapServers };
            _producer = new ProducerBuilder<EventType, string>(config)
                .Build();
        }

        public async Task PublishAsync<TEvent>(string topic, TEvent @event, CancellationToken cancellationToken = default)
            where TEvent : Event<object>
        {
            if (@event is null)
                throw new ArgumentNullException(nameof(@event));

            if (string.IsNullOrWhiteSpace(topic))
                throw new ArgumentNullException(nameof(topic));

            var serializedEvent = JsonSerializer.Serialize(@event);

            await _producer.ProduceAsync(topic, new Message<EventType, string>
            {
                Key = @event.EventType,
                Value = serializedEvent
            }, cancellationToken);
        }
    }
}