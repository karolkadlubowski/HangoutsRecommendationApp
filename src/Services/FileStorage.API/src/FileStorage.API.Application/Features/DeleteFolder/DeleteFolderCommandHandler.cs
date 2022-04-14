using System.Threading;
using System.Threading.Tasks;
using FileStorage.API.Application.Abstractions;
using Library.Shared.Logging;
using MediatR;

namespace FileStorage.API.Application.Features.DeleteFolder
{
    public class DeleteFolderCommandHandler : IRequestHandler<DeleteFolderCommand, DeleteFolderResponse>
    {
        private readonly IFolderService _folderService;
        private readonly IFileSystemAdapter _fileSystemAdapter;
        private readonly ILogger _logger;

        public DeleteFolderCommandHandler(IFolderService folderService,
            IFileSystemAdapter fileSystemAdapter,
            ILogger logger)
        {
            _folderService = folderService;
            _fileSystemAdapter = fileSystemAdapter;
            _logger = logger;
        }

        public async Task<DeleteFolderResponse> Handle(DeleteFolderCommand request, CancellationToken cancellationToken)
        {
            var deletedFolder = await _folderService.DeleteFolderAsync(request);

            if (await _fileSystemAdapter.DeleteFolderAsync(deletedFolder.Key))
            {
                _logger.Info($"Folder with the key '{deletedFolder.Key}' deleted from the server storage successfully");

                return new DeleteFolderResponse { DeletedFolderId = deletedFolder.FolderId, DeletedFolderKey = deletedFolder.Key };
            }

            _logger.Warning($"Folder with the key '{deletedFolder.Key}' deleted from the database but was not removed from the server storage");

            return new DeleteFolderResponse
            {
                DeletedFolderId = deletedFolder.FolderId,
                DeletedFolderKey = deletedFolder.Key
            };
        }
    }
}