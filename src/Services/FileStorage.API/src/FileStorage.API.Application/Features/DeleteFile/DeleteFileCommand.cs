using MediatR;

namespace FileStorage.API.Application.Features.DeleteFile
{
    public record DeleteFileCommand : IRequest<DeleteFileResponse>
    {
        public string FolderKey { get; init; }
        public string FileId { get; init; }
    }
}