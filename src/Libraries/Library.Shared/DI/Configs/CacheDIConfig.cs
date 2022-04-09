using Library.Shared.AppConfigs;
using Library.Shared.Caching;
using Library.Shared.Caching.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Library.Shared.DI.Configs
{
    public static class CacheDIConfig
    {
        public static IServiceCollection AddMemoryCache(this IServiceCollection services, IConfiguration configuration)
            => services
                .AddScoped(typeof(IMemoryCacheRepository<>), typeof(MemoryCacheRepository<>))
                .AddMemoryCache()
                .ConfigureCacheConfig(configuration);

        public static IServiceCollection AddDistributedCache(this IServiceCollection services, IConfiguration configuration)
            => services
                .AddScoped(typeof(IDistributedCacheRepository<>), typeof(DistributedCacheRepository<>))
                .AddStackExchangeRedisCache(opt =>
                    opt.Configuration = configuration.GetValue<string>(CacheConfig.DistributedCacheConnectionStringKey))
                .ConfigureCacheConfig(configuration);

        private static IServiceCollection ConfigureCacheConfig(this IServiceCollection services, IConfiguration configuration)
            => services
                .Configure<CacheConfig>(configuration.GetSection(nameof(CacheConfig)))
                .AddSingleton<CacheConfig>(s => s.GetRequiredService<IOptions<CacheConfig>>().Value);
    }
}