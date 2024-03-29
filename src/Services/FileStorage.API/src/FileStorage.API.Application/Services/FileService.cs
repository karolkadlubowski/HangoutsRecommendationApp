﻿using System.Threading.Tasks;
using AutoMapper;
using FileStorage.API.Application.Abstractions;
using FileStorage.API.Application.Database.PersistenceModels;
using FileStorage.API.Application.Database.Repositories;
using FileStorage.API.Application.Features.DeleteFile;
using FileStorage.API.Application.Features.GetFileByName;
using FileStorage.API.Application.Features.PutFile;
using FileStorage.API.Domain.Entities;
using FileStorage.API.Domain.ValueObjects;
using Library.Shared.Exceptions;
using Library.Shared.Logging;
using SimpleFileSystem.Abstractions;

namespace FileStorage.API.Application.Services
{
    public class FileService : IFileService
    {
        private readonly IFolderRepository _folderRepository;
        private readonly IFileSystemConfiguration _fileSystemConfiguration;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public FileService(IFolderRepository folderRepository,
            IFileSystemConfiguration fileSystemConfiguration,
            IMapper mapper,
            ILogger logger)
        {
            _folderRepository = folderRepository;
            _fileSystemConfiguration = fileSystemConfiguration;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<File> GetFileByNameAsync(GetFileByNameQuery query)
        {
            var folderPersistenceModel = await _folderRepository.GetFolderByKeyAsync(query.FolderKey)
                                         ?? throw new EntityNotFoundException($"Folder with the key: '{query.FolderKey}' not found in the database");

            _logger.Info($"Folder #{folderPersistenceModel.FolderId} with the key '{folderPersistenceModel.Key}' found in the database");

            var folder = _mapper.Map<Folder>(folderPersistenceModel);

            var fileName = new FileName(query.FileName);
            var file = folder.FindFileByName(fileName)
                       ?? throw new EntityNotFoundException($"File with the name '{fileName.Value}' not found in the folder with the key '{folder.Key}'");

            file.SetUrl(_fileSystemConfiguration.BaseUrl);

            _logger.Info($"File #{file.FileId} with the key '{file.Key}' found in the folder with the key '{folder.Key}'");

            return file;
        }

        public async Task<File> PutFileAsync(PutFileCommand command)
        {
            var folder = await GetOrCreateFolderFromDatabaseAsync(command.FolderKey);

            var file = File.CreateDefault(command.Name, folder);

            file.SetUrl(_fileSystemConfiguration.BaseUrl);

            folder.AddOrReplaceFile(file);
            _logger.Info(
                $"New file #{file.FileId} with key '{file.Key}' added to the folder #{folder.FolderId} with key '{folder.Key}'");

            if (!await _folderRepository.UpsertFolderAsync(
                    _mapper.Map<FolderPersistenceModel>(folder)))
                throw new DatabaseOperationException($"Upserting folder #{folder.FolderId} with the key '{folder.Key}' into the database failed");

            _logger.Info(
                $"Folder #{folder.FolderId} with the key '{folder.Key}' upserted into the database. Current files count: {folder.Files.Count}");

            return file;
        }

        public async Task<File> DeleteFileAndUpdateFolderAsync(DeleteFileCommand command)
        {
            var folderPersistenceModel = await _folderRepository.GetFolderByKeyAsync(command.FolderKey)
                                         ?? throw new EntityNotFoundException($"Specified folder with the key '{command.FolderKey}' does not exist in the database");

            var folder = _mapper.Map<Folder>(folderPersistenceModel);
            _logger.Trace($"Folder with the key '{folder.Key}' found in the database");

            var deletedFile = folder.DeleteFileIfExists(command.FileName);

            if (!await _folderRepository.UpdateFolderAsync(
                    _mapper.Map<FolderPersistenceModel>(folder)))
                throw new DatabaseOperationException($"Updating folder #{folder.FolderId} with the key '{folder.Key}' during file #{deletedFile.FileId} deletion in the database failed");

            _logger.Info($"File #{deletedFile.FileId} deleted from the database folder #{folder.FolderId} with the key '{folder.Key}' successfully");

            return deletedFile;
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