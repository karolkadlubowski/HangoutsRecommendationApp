using Library.Shared.AppConfigs;

namespace Venue.API.Domain.Configuration
{
    public record ServiceConfiguration
    {
        public DatabaseConfig DatabaseConfig { get; init; }
        public RestClientsConfig RestClientsConfig { get; init; }
    }
}