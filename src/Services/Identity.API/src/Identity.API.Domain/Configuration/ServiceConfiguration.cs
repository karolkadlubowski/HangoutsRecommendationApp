using Library.Shared.AppConfigs;

namespace Identity.API.Domain.Configuration
{
    public record ServiceConfiguration
    {
        public DatabaseConfig DatabaseConfig { get; set; }
        public JwtConfig JwtConfig { get; set; }
    }
}