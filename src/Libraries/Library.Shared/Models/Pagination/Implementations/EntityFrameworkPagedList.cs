using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Library.Shared.Models.Pagination.Implementations
{
    public class EntityFrameworkPagedList<T> : PagedList<T>
    {
        public EntityFrameworkPagedList(List<T> items, int count, int pageNumber, int pageSize)
            : base(items, count, pageNumber, pageSize)
        {
        }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}