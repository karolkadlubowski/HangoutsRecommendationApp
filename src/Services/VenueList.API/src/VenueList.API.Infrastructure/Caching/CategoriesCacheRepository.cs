using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Shared.AppConfigs;
using Library.Shared.Caching;
using Library.Shared.Exceptions;
using Library.Shared.Models.Category.Dtos;
using Microsoft.Extensions.Caching.Memory;
using VenueList.API.Application.Abstractions;
using VenueList.API.Domain.Configuration;

namespace VenueList.API.Infrastructure.Caching
{
    public class CategoriesCacheRepository : MemoryCacheRepository<IList<CategoryDto>>, ICategoriesCacheRepository
    {
        public CategoriesCacheRepository(IMemoryCache cache, CacheConfig cacheConfig)
            : base(cache, cacheConfig)
        {
        }

        public async Task<IReadOnlyList<CategoryDto>> GetCategoriesAsync()
            => (await GetValueOrDefaultAsync(CacheKeys.Categories))?.ToList()
               ?? new List<CategoryDto>();

        public async Task<CategoryDto> FindCategoryByNameAsync(string name)
            => (await GetCategoriesAsync())
                .FirstOrDefault(category => category.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
    }
}