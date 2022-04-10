using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Kafka.Public;
using Library.EventBus.Abstractions;
using Library.EventBus.AppConfigs;

namespace Library.EventBus
{
    public class KafkaEventConsumer : IEventConsumer
    {
        private readonly ClusterClient _clusterClient;

        public KafkaEventConsumer(KafkaSettings kafkaSettings)
        {
            _clusterClient = new ClusterClient(new Configuration
            {
                Seeds = kafkaSettings.BootstrapServers
            }, null);
        }

        public event EventHandler<Event> EventReceived;

        public async Task ConsumeFromLatestAsync(string topic, CancellationToken cancellationToken = default)
            => await Task.Factory.StartNew(() =>
            {
                _clusterClient.ConsumeFromLatest(topic);

                _clusterClient.MessageReceived += record =>
                {
                    var serializedEvent = Encoding.UTF8.GetString(record.Value as byte[] ?? Array.Empty<byte>());
                    var @event = JsonSerializer.Deserialize<Event>(serializedEvent);

                    if (serializedEvent != null)
                        EventReceived?.Invoke(this, @event);
                };
            });
    }
}