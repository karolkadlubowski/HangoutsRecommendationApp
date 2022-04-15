using MediatR;

namespace FileStorage.API.Application.Features.GetFolderByKey
{
    public record GetFolderByKeyQuery : IRequest<GetFolderByKeyResponse>
    {
        public string FolderKey { get; init; }
    }
}