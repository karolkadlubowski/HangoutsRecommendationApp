using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Library.Database;
using Library.Database.Abstractions;

namespace Venue.API.Application.Database.Repositories
{
    public interface IGenericRepository<TEntity> : IDbRepository<TEntity>
        where TEntity : BasePersistenceModel
    {
        Task<TEntity> FindByIdAsync(long entityId);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}