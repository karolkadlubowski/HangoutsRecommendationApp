using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace Library.Database.Abstractions
{
    public interface IDbContext
    {
        string ConnectionString { get; }

        Task<IEnumerable<T>> QueryAsync<T>(string query, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> QueryAsync<T>(string query, DynamicParameters parameters, CancellationToken cancellationToken = default);

        Task<int> ExecuteAsync(string query, CancellationToken cancellationToken = default);
        Task<int> ExecuteAsync(string query, DynamicParameters parameters, CancellationToken cancellationToken = default);
    }
}