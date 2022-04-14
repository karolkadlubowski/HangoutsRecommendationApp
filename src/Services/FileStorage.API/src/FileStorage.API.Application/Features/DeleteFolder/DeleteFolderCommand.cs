using MediatR;

namespace FileStorage.API.Application.Features.DeleteFolder
{
    public record DeleteFolderCommand : IRequest<DeleteFolderResponse>
    {
        public string FolderKey { get; init; }
    }
}