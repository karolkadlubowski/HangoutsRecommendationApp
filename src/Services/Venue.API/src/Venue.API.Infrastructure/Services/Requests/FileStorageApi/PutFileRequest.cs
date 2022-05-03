using Microsoft.AspNetCore.Http;

namespace Venue.API.Infrastructure.Services.Requests.FileStorageApi
{
    public record PutFileRequest : IRestRequestEndpoint
    {
        public string FolderKey { get; init; }
        public IFormFile File { get; init; }

        public string Endpoint => "file";
    }
}