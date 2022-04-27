using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Kafka.Public;
using Kafka.Public.Loggers;
using Library.EventBus.Abstractions;
using Library.EventBus.AppConfigs;

namespace Library.EventBus
{
    public class KafkaEventConsumer : IEventConsumer
    {
        private readonly ClusterClient _clusterClient;

        public KafkaEventConsumer(KafkaConfig kafkaConfig)
        {
            _clusterClient = new ClusterClient(new Configuration { Seeds = kafkaConfig.BootstrapServers }, new DevNullLogger());
        }

        public event EventHandler<Event> EventReceived;

        public async Task ConsumeFromLatestAsync(string topic, CancellationToken cancellationToken = default)
            => await Task.Factory.StartNew(() =>
            {
                _clusterClient.ConsumeFromLatest(topic);

                _clusterClient.MessageReceived += record => ReceiveEvent(record);
            });

        public async Task ConsumeFromEarliestAsync(string topic, CancellationToken cancellationToken = default)
            => await Task.Factory.StartNew(() =>
            {
                _clusterClient.ConsumeFromEarliest(topic);

                _clusterClient.MessageReceived += record => ReceiveEvent(record);
            });

        private void ReceiveEvent(RawKafkaRecord record)
        {
            var serializedEvent = Encoding.UTF8.GetString(record.Value as byte[] ?? Array.Empty<byte>());
            var @event = JsonSerializer.Deserialize<Event>(serializedEvent);

            if (serializedEvent != null)
                EventReceived?.Invoke(this, @event);
        }
    }
}