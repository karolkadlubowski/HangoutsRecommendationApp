using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FileStorage.API.Application.Abstractions;
using FileStorage.API.Application.Database.Repositories;
using FileStorage.API.Application.Features.DeleteFolder;
using FileStorage.API.Application.Features.GetFolderByKey;
using FileStorage.API.Domain.Entities;
using Library.Shared.Exceptions;
using Library.Shared.Logging;
using SimpleFileSystem.Abstractions;

namespace FileStorage.API.Application.Services
{
    public class FolderService : IFolderService
    {
        private readonly IFolderRepository _folderRepository;
        private readonly IFileSystemConfiguration _fileSystemConfiguration;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public FolderService(IFolderRepository folderRepository,
            IFileSystemConfiguration fileSystemConfiguration,
            IMapper mapper,
            ILogger logger)
        {
            _folderRepository = folderRepository;
            _fileSystemConfiguration = fileSystemConfiguration;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Folder> GetFolderByKeyAsync(GetFolderByKeyQuery query)
        {
            var folderPersistenceModel = await _folderRepository.GetFolderByKeyAsync(query.FolderKey)
                                         ?? throw new EntityNotFoundException($"Folder with key: '{query.FolderKey}' not found in the database");

            var folder = _mapper.Map<Folder>(folderPersistenceModel);

            folder.SetUrlForAllFiles(_fileSystemConfiguration.BaseUrl);

            _logger.Info($"Folder #{folder.FolderId} with key: '{folder.Key}' found in the database. It contains {folder.Files.Count} files");

            return folder;
        }

        public async Task<Folder> DeleteFolderWithSubfoldersAsync(DeleteFolderCommand command)
        {
            var folderPersistenceModel = await _folderRepository.GetFolderByKeyAsync(command.FolderKey)
                                         ?? throw new EntityNotFoundException($"Folder with key: '{command.FolderKey}' not found in the database");

            var folder = _mapper.Map<Folder>(folderPersistenceModel);

            if (!await _folderRepository.DeleteFolderAsync(folder.Key))
                throw new DatabaseOperationException($"Deleting folder with the key '{folder.FolderId}' from the database failed");

            _logger.Info($"Folder #{folder.FolderId} with the key '{folder.Key}' deleted from the database successfully");

            await DeleteSubfoldersFromDatabaseAsync(folder);

            return folder;
        }

        private async Task DeleteSubfoldersFromDatabaseAsync(Folder folder)
        {
            var subfoldersPersistenceModels = await _folderRepository.GetSubfoldersAsync(folder.Key);

            foreach (var subfolder in subfoldersPersistenceModels
                         .Where(subfolder => subfolder.FolderId != folder.FolderId))
            {
                _logger.Info($"Subfolder #{subfolder.FolderId} with the key '{subfolder.Key}' deleted from the database successfully");
                await _folderRepository.DeleteFolderAsync(subfolder.Key);
            }
        }
    }
}