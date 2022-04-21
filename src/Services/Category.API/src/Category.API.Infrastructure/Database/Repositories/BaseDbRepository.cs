using System;
using System.Linq;
using Category.API.Application.Database.Attributes;
using Library.Database;
using Library.Database.Abstractions;
using MongoDB.Driver;

namespace Category.API.Infrastructure.Database.Repositories
{
    public abstract class BaseDbRepository<TModel> : IDbRepository<TModel>
        where TModel : BasePersistenceModel
    {
        protected readonly IMongoCollection<TModel> _collection;

        protected BaseDbRepository(CategoryDbContext dbContext)
            => _collection = dbContext.Database.GetCollection<TModel>(GetCollectionName(typeof(TModel)));

        private protected string GetCollectionName(Type documentType)
            => ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
    }
}