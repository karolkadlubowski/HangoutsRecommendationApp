using Library.Shared.Clients;
using RestSharp;

namespace Venue.API.Infrastructure.Clients
{
    public class CategoryRestClient : BaseRestClient
    {
        protected override RestClient Client { get; }

        public CategoryRestClient(string categoryApiUrl)
            => Client = new RestClient(categoryApiUrl);
    }
}