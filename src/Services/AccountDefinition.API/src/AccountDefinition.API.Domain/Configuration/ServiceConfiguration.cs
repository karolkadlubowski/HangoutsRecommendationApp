using Library.Shared.AppConfigs;

namespace AccountDefinition.API.Domain.Configuration
{
    public record ServiceConfiguration
    {
        public DatabaseConfig DatabaseConfig { get; init; }
    }
}