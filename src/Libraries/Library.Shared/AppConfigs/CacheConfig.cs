namespace Library.Shared.AppConfigs
{
    public record CacheConfig
    {
        public const string DistributedCacheConnectionStringKey = "CacheConfig:DistributedCacheConnectionString";

        public int? SlidingExpirationMinutes { get; init; }
        public int? AbsoluteExpirationMinutes { get; init; }
        public string DistributedCacheConnectionString { get; init; }
    }
}