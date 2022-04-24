using Library.Shared.Models.FileStorage.Dtos;
using Library.Shared.Models.Response;

namespace Venue.API.Infrastructure.Services.Responses.FileStorageApi
{
    public record PutFileResponse : BaseResponse
    {
        public FileDto File { get; init; }

        public PutFileResponse(Error error = null) : base(error)
        {
        }
    }
}