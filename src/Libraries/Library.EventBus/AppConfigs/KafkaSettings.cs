namespace Library.EventBus.AppConfigs
{
    public record KafkaSettings
    {
        public string BootstrapServers { get; init; }
    }
}