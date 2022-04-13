using System.Threading.Tasks;
using AutoMapper;
using FileStorage.API.Application.Abstractions;
using FileStorage.API.Application.Database.PersistenceModels;
using FileStorage.API.Application.Database.Repositories;
using FileStorage.API.Application.Features.GetFileByName;
using FileStorage.API.Application.Features.PutFile;
using FileStorage.API.Domain.Entities;
using FileStorage.API.Domain.ValueObjects;
using Library.Shared.Exceptions;
using Library.Shared.Logging;
using Library.Shared.Models.FileStorage.Dtos;

namespace FileStorage.API.Application.Services
{
    public class FileService : IFileService
    {
        private readonly IFolderRepository _folderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public FileService(IFolderRepository folderRepository,
            IMapper mapper,
            ILogger logger)
        {
            _folderRepository = folderRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<FileDto> GetFileByNameAsync(GetFileByNameQuery query)
        {
            var folderKey = new FolderKey(query.FolderKey);

            var folderPersistenceModel = await _folderRepository.GetFolderByKeyAsync(folderKey)
                                         ?? throw new EntityNotFoundException($"Folder with the key: '{folderKey.Value}' not found in the database");

            _logger.Info($"Folder #{folderPersistenceModel.FolderId} with the key '{folderPersistenceModel.Key}' found in the database");

            var folder = _mapper.Map<Folder>(folderPersistenceModel);

            var fileName = new FileName(query.FileName);
            var file = folder.FindFileByName(fileName)
                       ?? throw new EntityNotFoundException($"File with the name '{fileName.Value}' not found in the folder with the key '{folder.Key}'");

            _logger.Info($"File #{file.FileId} with the key '{file.Key}' found in the folder with the key '{folder.Key}'");

            return _mapper.Map<FileDto>(file);
        }

        public async Task<FileDto> PutFileAsync(PutFileCommand command)
        {
            var folder = await GetOrCreateFolderFromDatabaseAsync(command.FolderKey);

            var file = File.CreateDefault(command.Name, folder);

            folder.AddOrReplaceFile(file);
            _logger.Info(
                $"New file #{file.FileId} with key '{file.Key}' added to the folder #{folder.FolderId} with key '{folder.Key}'");

            await _folderRepository.UpsertFolderAsync(
                _mapper.Map<FolderPersistenceModel>(folder));
            _logger.Info(
                $"Folder #{folder.FolderId} with key '{folder.Key}' upserted into the database. Current files count: {folder.Files.Count}");

            return _mapper.Map<FileDto>(_mapper.Map<File>(file));
        }

        private async Task<Folder> GetOrCreateFolderFromDatabaseAsync(string folderKey)
        {
            var folder = Folder.CreateDefault(folderKey);

            var folderPersistenceModel = await _folderRepository.GetFolderByKeyAsync(folder.Key);

            if (folderPersistenceModel is not null)
            {
                _logger.Trace($"Folder #{folderPersistenceModel.FolderId} with key '{folder.Key}' found in the database. Updating entry");
                folder = _mapper.Map<Folder>(folderPersistenceModel);
                folder.UpdateNow();
            }

            _logger.Trace($"Folder with key '{folder.Key}' not found in the database. Inserting entry");

            return folder;
        }
    }
}