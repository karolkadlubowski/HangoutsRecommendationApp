using Library.Shared.Models.FileStorage.Dtos;
using Library.Shared.Models.Response;

namespace Venue.API.Infrastructure.Services.Responses.FileStorageApi
{
    public record GetFolderResponse : BaseResponse
    {
        public FolderDto Folder { get; init; }

        public GetFolderResponse()
        {
        }
    }
}