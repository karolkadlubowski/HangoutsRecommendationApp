using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Shared.Models.Category.Dtos;

namespace VenueList.API.Application.Abstractions
{
    public interface ICategoryDataService
    {
        Task<IList<CategoryDto>> GetCategoriesAsync();
        Task StoreCategoriesInCacheAsync(IList<CategoryDto> categories);
    }
}