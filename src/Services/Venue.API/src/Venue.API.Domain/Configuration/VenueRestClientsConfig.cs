using Library.Shared.AppConfigs;

namespace Venue.API.Domain.Configuration
{
    public record VenueRestClientsConfig : RestClientsConfig
    {
        public RestClientConfig CategoryApi { get; init; }
        public RestClientConfig FileStorageApi { get; init; }
    }
}