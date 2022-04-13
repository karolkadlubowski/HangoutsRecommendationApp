using MediatR;

namespace FileStorage.API.Application.Features.GetFileByName
{
    public record GetFileByNameQuery : IRequest<GetFileByNameResponse>
    {
        public string FileName { get; init; }
        public string FolderKey { get; init; }
    }
}