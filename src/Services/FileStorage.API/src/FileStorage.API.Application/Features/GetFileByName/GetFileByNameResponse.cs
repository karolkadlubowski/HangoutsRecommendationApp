using Library.Shared.Models.FileStorage.Dtos;
using Library.Shared.Models.Response;

namespace FileStorage.API.Application.Features.GetFileByName
{
    public record GetFileByNameResponse : BaseApiResponse
    {
        public FileDto File { get; init; }

        public GetFileByNameResponse(Error error = null) : base(error)
        {
        }
    }
}