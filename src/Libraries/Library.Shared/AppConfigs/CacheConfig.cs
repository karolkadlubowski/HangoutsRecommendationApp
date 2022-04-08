namespace Library.Shared.AppConfigs
{
    public record CacheConfig
    {
        public const string ConnectionString = "ConnectionStrings:DistributedCacheConnectionString";

        public int? SlidingExpirationMinutes { get; init; }
        public int? AbsoluteExpirationMinutes { get; init; }
    }
}