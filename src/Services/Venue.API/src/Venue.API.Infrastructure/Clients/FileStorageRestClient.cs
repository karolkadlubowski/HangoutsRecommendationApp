using Library.Shared.Clients;
using RestSharp;

namespace Venue.API.Infrastructure.Clients
{
    public class FileStorageRestClient : BaseRestClient
    {
        protected override RestClient Client { get; }

        public FileStorageRestClient(string fileStorageApiUrl)
            => Client = new RestClient(fileStorageApiUrl);
    }
}