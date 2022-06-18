namespace VenueList.API.Domain.Configuration
{
    public record ServiceConfiguration
    {
        public DatabaseConfig DatabaseConfig { get; init; }
        public VenueRestClientsConfig RestClientsConfig { get; init; }
        public HostedServicesConfig HostedServicesConfig { get; init; }
        public VenueRetryPoliciesConfig RetryPoliciesConfig { get; init; }
    }
}