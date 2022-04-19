using System.Threading.Tasks;
using Category.API.Application.Features.AddCategory;
using Category.API.Application.Features.DeleteCategory;

namespace Category.API.Application.Abstractions
{
    public interface ICategoryService : IReadOnlyCategoryService
    {
        Task<Domain.Entities.Category> AddCategoryAsync(AddCategoryCommand command);
        Task<string> DeleteCategoryAsync(DeleteCategoryCommand command);
    }
}