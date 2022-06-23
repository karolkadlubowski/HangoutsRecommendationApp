using Library.Shared.Clients.Abstractions;
using Library.Shared.Clients.Factories;

namespace VenueList.API.Infrastructure.Clients.Factories
{
    public class CategoryRestClientFactory : IRestClientFactory
    {
        public IRestClient CreateRestClient(string baseApiUrl)
            => new CategoryRestClient(baseApiUrl);
    }
}