using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Library.Shared.Models.Pagination.Implementations
{
    public class MongoDbPagedList<T> : PagedList<T>
    {
        public MongoDbPagedList(List<T> items, int count, int pageNumber, int pageSize)
            : base(items, count, pageNumber, pageSize)
        {
        }

        public static async Task<PagedList<T>> CreateAsync(IMongoQueryable<T> source, int pageNumber, int pageSize)
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