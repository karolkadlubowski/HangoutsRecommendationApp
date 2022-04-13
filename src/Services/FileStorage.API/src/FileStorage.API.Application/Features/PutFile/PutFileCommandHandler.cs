using System.Threading;
using System.Threading.Tasks;
using FileStorage.API.Application.Abstractions;
using Library.Shared.Exceptions;
using Library.Shared.Logging;
using MediatR;
using SimpleFileSystem.Extensions;

namespace FileStorage.API.Application.Features.PutFile
{
    public class PutFileCommandHandler : IRequestHandler<PutFileCommand, PutFileResponse>
    {
        private readonly IFileService _fileService;
        private readonly IFileSystemFacade _fileSystemFacade;
        private readonly ILogger _logger;

        public PutFileCommandHandler(IFileService fileService,
            IFileSystemFacade fileSystemFacade,
            ILogger logger)
        {
            _fileService = fileService;
            _fileSystemFacade = fileSystemFacade;
            _logger = logger;
        }

        public async Task<PutFileResponse> Handle(PutFileCommand request, CancellationToken cancellationToken)
        {
            var file = await _fileService.PutFileAsync(request);

            if (file is not null)
            {
                _logger.Info($"File entry #{file.FileId} with the key '{file.Key}' written to the database successfully");

                var uploadedFileModel = await _fileSystemFacade.UploadAsync(request.File, file.FolderKey.CleanPath());

                if (uploadedFileModel is not null)
                {
                    _logger.Info($"File with the key '{file.Key}' uploaded to the server storage successfully");

                    return new PutFileResponse { File = file with { FileUrl = uploadedFileModel.Url } };
                }

                throw new ServerException($"Uploading file with the key '{file.Key}' to the server storage failed but entry is stored in the database");
            }

            throw new DatabaseOperationException($"Writing file entry with the key '{file.Key}' to the database failed");
        }
    }
}