using System.Collections.Generic;
using System.Threading.Tasks;
using Category.API.Application.Database.PersistenceModels;

namespace Category.API.Application.Database.Repositories
{
    public interface ICategoryRepository
    {
        Task<IReadOnlyList<CategoryPersistenceModel>> GetAllCategoriesAsync();

        Task<CategoryPersistenceModel> InsertCategoryAsync(string name);
    }
}