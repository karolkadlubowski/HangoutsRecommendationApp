using Library.Shared.Models.FileStorage.Dtos;
using Library.Shared.Models.Response;

namespace FileStorage.API.Application.Features.PutFile
{
    public record PutFileResponse : BaseResponse
    {
        public FileDto File { get; init; }

        public PutFileResponse(Error error = null) : base(error)
        {
        }
    }
}