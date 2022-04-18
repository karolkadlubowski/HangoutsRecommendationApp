using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Category.API.Application.Database.PersistenceModels;
using Category.API.Application.Database.Repositories;
using MongoDB.Driver;

namespace Category.API.Infrastructure.Database.Repositories
{
    public class CategoryRepository : BaseDbRepository<CategoryPersistenceModel>, ICategoryRepository
    {
        public CategoryRepository(CategoryDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<CategoryPersistenceModel>> GetAllCategoriesAsync()
            => await _collection
                .AsQueryable()
                .ToListAsync();

        public async Task<CategoryPersistenceModel> InsertCategoryAsync(string name)
        {
            var category = new CategoryPersistenceModel { Name = name, CreatedOn = DateTime.UtcNow };

            await _collection
                .InsertOneAsync(category);

            return category;
        }
    }
}