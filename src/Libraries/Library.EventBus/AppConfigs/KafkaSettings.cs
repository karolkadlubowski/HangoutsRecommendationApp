namespace Library.EventBus.AppConfigs
{
    public record KafkaConfig
    {
        public string BootstrapServers { get; init; }
    }
}