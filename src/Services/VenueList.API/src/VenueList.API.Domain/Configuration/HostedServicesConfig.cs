namespace VenueList.API.Domain.Configuration
{
    public record HostedServicesConfig
    {
        public int CategoryDataHostedServiceIntervalInMinutes { get; init; } = 1440;
    }
}