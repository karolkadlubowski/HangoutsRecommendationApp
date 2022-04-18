using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Category.API.Application.Database.PersistenceModels;
using Category.API.Application.Database.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Category.API.Infrastructure.Database.Repositories
{
    public class CategoryRepository : BaseDbRepository<CategoryPersistenceModel>, ICategoryRepository
    {
        public CategoryRepository(CategoryDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<CategoryPersistenceModel>> GetCategoriesOrderedByNameAsync()
            => await _collection.AsQueryable()
                .OrderBy(c => c.Name)
                .ToListAsync();

        public async Task<CategoryPersistenceModel> InsertCategoryAsync(string name)
        {
            try
            {
                var category = new CategoryPersistenceModel { Name = name, CreatedOn = DateTime.UtcNow };

                await _collection
                    .InsertOneAsync(category);

                return category;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DoesCategoryExist(string name)
            => await _collection.AsQueryable()
                .AnyAsync(c => c.Name == name);
    }
}