using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Shared.Caching.Abstractions;
using Library.Shared.Models.Category.Dtos;

namespace Venue.API.Application.Abstractions
{
    public interface ICategoriesCacheRepository : IMemoryCacheRepository<IList<CategoryDto>>
    {
        Task StoreCategoryAsync(CategoryDto category);
    }
}