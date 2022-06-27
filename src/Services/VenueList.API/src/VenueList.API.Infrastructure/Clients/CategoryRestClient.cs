using Library.Shared.Clients;
using RestSharp;

namespace VenueList.API.Infrastructure.Clients
{
    public class CategoryRestClient : BaseRestClient
    {
        protected override RestClient Client { get; }

        public CategoryRestClient(string categoryApiUrl)
            => Client = new RestClient(categoryApiUrl);
    }
}