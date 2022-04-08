using Library.Shared.Clients.Abstractions;

namespace Library.Shared.Clients.Factories
{
    public interface IRestClientFactory
    {
        IRestClient CreateRestClient(string baseApiUrl);
    }
}