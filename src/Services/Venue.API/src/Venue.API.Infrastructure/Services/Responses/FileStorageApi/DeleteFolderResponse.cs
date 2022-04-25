using Library.Shared.Models.Response;

namespace Venue.API.Infrastructure.Services.Responses.FileStorageApi
{
    public record DeleteFolderResponse : BaseResponse
    {
        public DeleteFolderResponse(Error error = null) : base(error)
        {
        }
    }
}