using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Shared.AppConfigs;
using Library.Shared.Caching;
using Library.Shared.Models.Category.Dtos;
using Microsoft.Extensions.Caching.Memory;
using Venue.API.Application.Abstractions;
using Venue.API.Domain.Configuration;

namespace Venue.API.Infrastructure.Caching
{
    public class CategoriesCacheRepository : MemoryCacheRepository<IList<CategoryDto>>, ICategoriesCacheRepository
    {
        public CategoriesCacheRepository(IMemoryCache cache, CacheConfig cacheConfig)
            : base(cache, cacheConfig)
        {
        }

        public async Task<IReadOnlyList<CategoryDto>> GetCategoriesAsync()
            => (await GetValueOrDefaultAsync(Constants.CacheKeys.Categories))?.ToList()
               ?? new List<CategoryDto>();

        public async Task StoreCategoryAsync(CategoryDto category)
        {
            var categories = await GetValueOrDefaultAsync(Constants.CacheKeys.Categories)
                             ?? new List<CategoryDto>();

            categories.Add(category);

            await SetValueAsync(Constants.CacheKeys.Categories, categories);
        }
    }
}