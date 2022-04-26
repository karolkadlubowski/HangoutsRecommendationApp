namespace Venue.API.Infrastructure.Services.Requests.FileStorageApi
{
    public record GetFolderRequest : IRestRequestEndpoint
    {
        public string FolderKey { get; init; }

        public string Endpoint => "folder";
    }
}