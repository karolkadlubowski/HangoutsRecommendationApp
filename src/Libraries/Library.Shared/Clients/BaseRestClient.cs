using System.Threading;
using System.Threading.Tasks;
using RestSharp;
using IRestClient = Library.Shared.Clients.Abstractions.IRestClient;

namespace Library.Shared.Clients
{
    public abstract class BaseRestClient : IRestClient
    {
        protected abstract RestClient Client { get; }

        public async Task<IRestResponse<T>> ExecuteAsync<T>(IRestRequest request, CancellationToken cancellationToken = default)
            => await Client.ExecuteAsync<T>(request, cancellationToken).ConfigureAwait(false);
    }
}