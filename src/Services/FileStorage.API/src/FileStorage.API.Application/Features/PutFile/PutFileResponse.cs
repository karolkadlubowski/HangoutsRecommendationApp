using Library.Shared.Models.FileStorage.Dtos;
using Library.Shared.Models.Response;

namespace FileStorage.API.Application.Features.PutFile
{
    public record PutFileResponse : BaseApiResponse
    {
        public FileInformationDto FileInformation { get; init; }

        public PutFileResponse(Error error = null) : base(error)
        {
        }
    }
}