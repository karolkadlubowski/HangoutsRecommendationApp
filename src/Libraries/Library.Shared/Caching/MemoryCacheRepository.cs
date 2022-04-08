using System.Threading.Tasks;
using Library.Shared.AppConfigs;
using Library.Shared.Caching.Abstractions;
using Library.Shared.Caching.Factories;
using Microsoft.Extensions.Caching.Memory;

namespace Library.Shared.Caching
{
    public class MemoryCacheRepository<T> : IMemoryCacheRepository<T> where T : class
    {
        protected readonly IMemoryCache _cache;
        private readonly CacheConfig _cacheConfig;

        public MemoryCacheRepository(IMemoryCache cache, CacheConfig cacheConfig)
        {
            _cache = cache;
            _cacheConfig = cacheConfig;
        }

        public virtual async Task<T> GetValueOrDefaultAsync(string key)
        {
            _cache.TryGetValue(key, out T entry);

            return await Task.FromResult(entry);
        }

        public virtual async Task SetValueAsync(string key, T value)
            => await Task.FromResult(_cache.Set(key, value,
                CacheEntryOptionsAbstractFactory.CreateMemoryCacheEntryOptions(_cacheConfig)));

        public virtual async Task DeleteValueAsync(string key)
            => await Task.Run(() => _cache.Remove(key));
    }
}