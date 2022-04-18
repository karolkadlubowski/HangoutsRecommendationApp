using System.Threading.Tasks;
using Category.API.Application.Features.AddCategory;

namespace Category.API.Application.Abstractions
{
    public interface ICategoryService : IReadOnlyCategoryService
    {
        Task<Domain.Entities.Category> AddCategoryAsync(AddCategoryCommand command);
    }
}