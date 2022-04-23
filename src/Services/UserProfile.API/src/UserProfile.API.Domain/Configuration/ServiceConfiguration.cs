namespace UserProfile.API.Domain.Configuration
{
    public record ServiceConfiguration
    {
        public HostedServiceConfig HostedServiceConfig { get; init; }
    }
}