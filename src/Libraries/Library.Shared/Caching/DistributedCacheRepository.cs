using System.Threading.Tasks;
using Library.Shared.AppConfigs;
using Library.Shared.Caching.Abstractions;
using Library.Shared.Caching.Factories;
using Library.Shared.Extensions;
using Microsoft.Extensions.Caching.Distributed;

namespace Library.Shared.Caching
{
    public class DistributedCacheRepository<T> : IDistributedCacheRepository<T> where T : class
    {
        protected readonly IDistributedCache _cache;
        private readonly CacheConfig _cacheConfig;

        public DistributedCacheRepository(IDistributedCache cache, CacheConfig cacheConfig)
        {
            _cache = cache;
            _cacheConfig = cacheConfig;
        }

        public virtual async Task<T> GetValueOrDefaultAsync(string key)
        {
            var value = await _cache.GetStringAsync(key);

            return string.IsNullOrEmpty(value) ? null : value.FromJSON<T>();
        }

        public virtual async Task SetValueAsync(string key, T value)
            => await _cache.SetStringAsync(key, value.ToJSON(),
                CacheEntryOptionsAbstractFactory.CreateDistributedCacheEntryOptions(_cacheConfig));

        public virtual async Task DeleteValueAsync(string key)
            => await _cache.RemoveAsync(key);
    }
}