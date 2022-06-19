using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Library.Shared.Models.Pagination;
using Library.Shared.Models.Pagination.Implementations;
using MongoDB.Driver.Linq;

namespace Library.Shared.Extensions
{
    public static class IQueryableExtensions
    {
        public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> queryable, int pageNumber, int pageSize)
            => await EntityFrameworkPagedList<T>.CreateAsync(queryable, pageNumber, pageSize);
        
        public static async Task<IPagedList<T>> ToMongoPagedListAsync<T>(this IMongoQueryable<T> queryable, int pageNumber, int pageSize)
            => await MongoDbPagedList<T>.CreateAsync(queryable, pageNumber, pageSize);

        public static IQueryable<TEntity> WhereIf<TEntity>(this IQueryable<TEntity> query, bool condition,
            Expression<Func<TEntity, bool>> predicate)
            => condition
                ? query.Where(predicate)
                : query;
    }
}