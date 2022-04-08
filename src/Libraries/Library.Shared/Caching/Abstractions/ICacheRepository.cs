using System.Threading.Tasks;

namespace Library.Shared.Caching.Abstractions
{
    public interface ICacheRepository<T>
    {
        Task<T> GetValueOrDefaultAsync(string key);
        Task SetValueAsync(string key, T value);
        Task DeleteValueAsync(string key);
    }
}