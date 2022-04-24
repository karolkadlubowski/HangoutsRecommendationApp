using System.Linq;
using System.Threading.Tasks;
using Library.Shared.Models.Pagination;
using Library.Shared.Models.Pagination.Implementations;

namespace Library.Shared.Extensions
{
    public static class IQueryableExtensions
    {
        public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> queryable, int pageNumber, int pageSize)
            => await EntityFrameworkPagedList<T>.CreateAsync(queryable, pageNumber, pageSize);
    }
}