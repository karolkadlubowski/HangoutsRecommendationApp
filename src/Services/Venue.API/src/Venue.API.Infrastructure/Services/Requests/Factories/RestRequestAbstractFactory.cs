using RestSharp;
using Venue.API.Infrastructure.Services.Requests.CategoryApi;
using Venue.API.Infrastructure.Services.Requests.FileStorageApi;

namespace Venue.API.Infrastructure.Services.Requests.Factories
{
    public abstract class RestRequestAbstractFactory
    {
        public static IRestRequest GetCategoriesRequest(GetCategoriesRequest request)
            => new RestRequest(request.Endpoint, Method.GET);

        public static IRestRequest GetFolderRequest(GetFolderRequest request)
            => new RestRequest(request.Endpoint, Method.GET)
                .AddQueryParameter(nameof(request.FolderKey), request.FolderKey);

        public static IRestRequest PutFileRequest(PutFileRequest request)
            => new RestRequest(request.Endpoint, Method.PUT)
                .AddParameter(nameof(request.FolderKey), request.FolderKey)
                .AddFile(nameof(request.File), writer: s =>
                {
                    var stream = request.File.OpenReadStream();
                    stream.CopyTo(s);
                    stream.Dispose();
                }, request.File.FileName, request.File.Length, "multipart/form-data");

        public static IRestRequest DeleteFolderRequest(DeleteFolderRequest request)
            => new RestRequest(request.Endpoint, Method.DELETE)
                .AddQueryParameter(nameof(request.FolderKey), request.FolderKey);
    }
}