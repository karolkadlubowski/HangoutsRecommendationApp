using Library.Shared.AppConfigs;

namespace VenueList.API.Domain.Configuration
{
    public record VenueRestClientsConfig : RestClientsConfig
    {
        public RestClientConfig CategoryApi { get; init; }
    }
}