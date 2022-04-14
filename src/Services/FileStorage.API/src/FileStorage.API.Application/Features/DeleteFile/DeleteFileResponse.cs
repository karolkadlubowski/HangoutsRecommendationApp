using Library.Shared.Models.Response;

namespace FileStorage.API.Application.Features.DeleteFile
{
    public record DeleteFileResponse : BaseApiResponse
    {
        public string DeletedFileId { get; init; }

        public DeleteFileResponse(Error error = null) : base(error)
        {
        }
    }
}