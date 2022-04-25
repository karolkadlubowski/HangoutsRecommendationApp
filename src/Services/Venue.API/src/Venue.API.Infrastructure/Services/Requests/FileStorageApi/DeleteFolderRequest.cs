namespace Venue.API.Infrastructure.Services.Requests.FileStorageApi
{
    public record DeleteFolderRequest : IRestRequestEndpoint
    {
        public string FolderKey { get; init; }

        public string Endpoint => "folder";
    }
}