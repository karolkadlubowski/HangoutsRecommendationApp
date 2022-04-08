namespace Library.Shared.AppConfigs
{
    public record CacheConfig
    {
        public const string ConnectionString = "CacheConfig:DistributedCacheConnectionString";

        public int? SlidingExpirationMinutes { get; init; }
        public int? AbsoluteExpirationMinutes { get; init; }
        public string DistributedCacheConnectionString { get; init; }
    }
}