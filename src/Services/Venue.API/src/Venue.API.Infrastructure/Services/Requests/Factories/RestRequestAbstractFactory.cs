using RestSharp;
using Venue.API.Infrastructure.Services.Requests.CategoryApi;
using Venue.API.Infrastructure.Services.Requests.FileStorageApi;

namespace Venue.API.Infrastructure.Services.Requests.Factories
{
    public abstract class RestRequestAbstractFactory
    {
        public static IRestRequest GetCategoriesRequest(GetCategoriesRequest request)
            => new RestRequest(request.Endpoint, Method.GET);

        public static IRestRequest PutFileRequest(PutFileRequest request)
            => new RestRequest(Method.PUT)
                .AddParameter(nameof(request.FolderKey), request.FolderKey)
                .AddParameter(nameof(request.File), request.File);
    }
}