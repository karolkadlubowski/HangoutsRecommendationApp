using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace Library.Database.Abstractions
{
    public interface IDbContext
    {
        Task ExecuteAsync(string query, CancellationToken cancellationToken = default);
        Task ExecuteAsync(string query, DynamicParameters parameters, CancellationToken cancellationToken = default);
    }
}