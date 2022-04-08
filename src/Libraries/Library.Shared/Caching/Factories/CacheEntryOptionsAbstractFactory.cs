using System;
using Library.Shared.AppConfigs;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace Library.Shared.Caching.Factories
{
    public abstract class CacheEntryOptionsAbstractFactory
    {
        public static MemoryCacheEntryOptions CreateMemoryCacheEntryOptions(CacheConfig cacheConfig)
        {
            var memoryCacheEntryOptions = new MemoryCacheEntryOptions();

            if (cacheConfig.SlidingExpirationMinutes.HasValue)
                memoryCacheEntryOptions.SlidingExpiration =
                    TimeSpan.FromMinutes(cacheConfig.SlidingExpirationMinutes.Value);

            if (cacheConfig.AbsoluteExpirationMinutes.HasValue)
                memoryCacheEntryOptions.AbsoluteExpiration =
                    DateTime.UtcNow.AddMinutes(cacheConfig.AbsoluteExpirationMinutes.Value);

            return memoryCacheEntryOptions;
        }

        public static DistributedCacheEntryOptions CreateDistributedCacheEntryOptions(CacheConfig cacheConfig)
        {
            var distributedCacheEntryOptions = new DistributedCacheEntryOptions();

            if (cacheConfig.SlidingExpirationMinutes.HasValue)
                distributedCacheEntryOptions.SlidingExpiration =
                    TimeSpan.FromMinutes(cacheConfig.SlidingExpirationMinutes.Value);

            if (cacheConfig.AbsoluteExpirationMinutes.HasValue)
                distributedCacheEntryOptions.AbsoluteExpiration =
                    DateTime.UtcNow.AddMinutes(cacheConfig.AbsoluteExpirationMinutes.Value);

            return distributedCacheEntryOptions;
        }
    }
}