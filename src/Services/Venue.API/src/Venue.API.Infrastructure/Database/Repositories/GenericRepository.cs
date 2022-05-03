using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Library.Database;
using Microsoft.EntityFrameworkCore;
using Venue.API.Application.Database.Repositories;

namespace Venue.API.Infrastructure.Database.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : BasePersistenceModel
    {
        protected readonly VenueDbContext _dbContext;

        public GenericRepository(VenueDbContext dbContext) => _dbContext = dbContext;

        public async Task<TEntity> FindByIdAsync(long entityId)
            => await _dbContext.Set<TEntity>().FindAsync(entityId);

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
            => await _dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);

        public void Add(TEntity entity)
            => _dbContext.Set<TEntity>().Add(entity);

        public void Update(TEntity entity)
            => _dbContext.Set<TEntity>().Update(entity);

        public void Delete(TEntity entity)
            => _dbContext.Set<TEntity>().Remove(entity);
    }
}