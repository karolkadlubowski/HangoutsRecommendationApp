using MediatR;

namespace FileStorage.API.Application.Features.GetFolderByKey
{
    public record GetFolderByKeyQuery : IRequest<GetFolderByKeyResponse>
    {
        public string Key { get; init; }
    }
}