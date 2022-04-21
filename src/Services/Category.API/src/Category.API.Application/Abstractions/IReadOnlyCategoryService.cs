using System.Collections.Generic;
using System.Threading.Tasks;

namespace Category.API.Application.Abstractions
{
    public interface IReadOnlyCategoryService
    {
        Task<IReadOnlyList<Domain.Entities.Category>> GetCategoriesAsync();
    }
}