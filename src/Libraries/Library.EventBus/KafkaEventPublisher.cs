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
        private readonly IProducer<Null, string> _producer;

        public KafkaEventPublisher(KafkaConfig kafkaConfig)
        {
            var config = new ProducerConfig { BootstrapServers = kafkaConfig.BootstrapServers };
            _producer = new ProducerBuilder<Null, string>(config)
                .Build();
        }

        public async Task PublishAsync<TEvent>(string topic, TEvent @event, CancellationToken cancellationToken = default)
            where TEvent : Event
        {
            if (@event is null)
                throw new ArgumentNullException(nameof(@event));

            if (string.IsNullOrWhiteSpace(topic))
                throw new ArgumentNullException(nameof(topic));

            var serializedEvent = JsonSerializer.Serialize(@event);

            await _producer.ProduceAsync(topic, new Message<Null, string>
            {
                Value = serializedEvent
            }, cancellationToken);
        }
    }
}