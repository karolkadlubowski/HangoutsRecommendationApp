using System.Threading;
using System.Threading.Tasks;
using FileStorage.API.Application.Abstractions;
using Library.Shared.Logging;
using MediatR;

namespace FileStorage.API.Application.Features.DeleteFile
{
    public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, DeleteFileResponse>
    {
        private readonly IFileService _fileService;
        private readonly IFileSystemAdapter _fileSystemAdapter;
        private readonly ILogger _logger;

        public DeleteFileCommandHandler(IFileService fileService,
            IFileSystemAdapter fileSystemAdapter,
            ILogger logger)
        {
            _fileService = fileService;
            _fileSystemAdapter = fileSystemAdapter;
            _logger = logger;
        }

        public async Task<DeleteFileResponse> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var deletedFile = await _fileService.DeleteFileAndUpdateFolderAsync(request);

            if (await _fileSystemAdapter.DeleteFileAsync(deletedFile.Key))
            {
                _logger.Info($"File with the key '{deletedFile.Key}' deleted from the server storage successfully");

                return new DeleteFileResponse { DeletedFileId = deletedFile.FileId };
            }

            _logger.Warning($"File with the key '{deletedFile.Key}' deleted from the database but was not removed from the server storage");

            return new DeleteFileResponse { DeletedFileId = deletedFile.FileId };
        }
    }
}