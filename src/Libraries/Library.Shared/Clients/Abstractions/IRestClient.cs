using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace Library.Shared.Clients.Abstractions
{
    public interface IRestClient
    {
        Task<IRestResponse<T>> ExecuteAsync<T>(IRestRequest request, CancellationToken cancellationToken = default);
    }
}