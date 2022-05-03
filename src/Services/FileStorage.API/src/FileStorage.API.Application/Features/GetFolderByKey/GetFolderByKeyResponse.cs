using Library.Shared.Models.FileStorage.Dtos;
using Library.Shared.Models.Response;

namespace FileStorage.API.Application.Features.GetFolderByKey
{
    public record GetFolderByKeyResponse : BaseResponse
    {
        public FolderDto Folder { get; init; }

        public GetFolderByKeyResponse(Error error = null) : base(error)
        {
        }
    }
}