namespace Library.Shared.AppConfigs
{
    public record DatabaseConfig
    {
        public string DatabaseConnectionString { get; init; }
    }
}