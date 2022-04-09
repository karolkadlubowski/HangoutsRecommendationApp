using System;
using System.Text;
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

        public event EventHandler<EventType> EventReceived;

        public async Task ConsumeFromLatestAsync(string topic, CancellationToken cancellationToken = default)
            => await Task.Factory.StartNew(() =>
            {
                _clusterClient.ConsumeFromLatest(topic);

                _clusterClient.MessageReceived += record =>
                {
                    var eventType = Enum.Parse<EventType>(record.Key.ToString() ?? string.Empty);
                    var serializedEvent = Encoding.UTF8.GetString(record.Value as byte[] ?? Array.Empty<byte>());

                    if (serializedEvent != null)
                        EventReceived?.Invoke(this, eventType);
                };
            });
    }
}