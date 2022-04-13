using System.IO;
using MediatR;
using Microsoft.AspNetCore.Http;
using SimpleFileSystem.Extensions;

namespace FileStorage.API.Application.Features.PutFile
{
    public record PutFileCommand : IRequest<PutFileResponse>
    {
        public string FolderKey { get; init; }
        public IFormFile File { get; init; }

        public string Name => File?.GetFileNameWithExtension();
    }
}