namespace Category.API.Domain.Configuration
{
    public record ServiceConfiguration
    {
        public DatabaseConfig DatabaseConfig { get; init; }
    }
}