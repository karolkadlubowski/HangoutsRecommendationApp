using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FileStorage.API.Application.Abstractions;
using FileStorage.API.Domain.ValueObjects;
using Library.Shared.Exceptions;
using Library.Shared.Logging;
using Library.Shared.Models.FileStorage.Dtos;
using MediatR;

namespace FileStorage.API.Application.Features.PutFile
{
    public class PutFileCommandHandler : IRequestHandler<PutFileCommand, PutFileResponse>
    {
        private readonly IFileService _fileService;
        private readonly IFileSystemAdapter _fileSystemAdapter;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public PutFileCommandHandler(IFileService fileService,
            IFileSystemAdapter fileSystemAdapter,
            IMapper mapper,
            ILogger logger)
        {
            _fileService = fileService;
            _fileSystemAdapter = fileSystemAdapter;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PutFileResponse> Handle(PutFileCommand request, CancellationToken cancellationToken)
        {
            var folderKey = new FolderKey(request.FolderKey).Value;
            var uploadedFileModel = await _fileSystemAdapter.UploadAsync(request.File, folderKey);

            try
            {
                if (uploadedFileModel is not null)
                {
                    _logger.Info($"File under the path '{uploadedFileModel.Path}' uploaded to the server storage successfully");

                    var file = await _fileService.PutFileAsync(request);

                    if (file is not null)
                    {
                        _logger.Info($"File entry #{file.FileId} with the key '{file.Key}' written to the database successfully");

                        var fileToReturn = _mapper.Map<FileDto>(file);

                        return new PutFileResponse { File = fileToReturn with { FileUrl = uploadedFileModel.Url } };
                    }

                    throw new DatabaseOperationException($"Writing file entry with the folder key '{folderKey}' to the database failed");
                }

                throw new ServerException($"Uploading file with the folder key '{folderKey}' to the server storage failed");
            }
            catch (DatabaseOperationException)
            {
                if (await _fileSystemAdapter.DeleteFileAsync(uploadedFileModel.Path))
                    _logger.Warning($"File under the path '{uploadedFileModel.Path}' deleted from the storage successfully");

                throw;
            }
        }
    }
}