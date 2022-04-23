namespace UserProfile.API.Domain.Configuration
{
    public record HostedServiceConfig
    {
        public int EventConsumerDelayInMinutes { get; init; }
    }
}