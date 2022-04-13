using System.Threading.Tasks;
using AutoMapper;
using FileStorage.API.Application.Abstractions;
using FileStorage.API.Application.Database.PersistenceModels;
using FileStorage.API.Application.Database.Repositories;
using FileStorage.API.Application.Features.PutFile;
using FileStorage.API.Domain.Entities;
using Library.Shared.Logging;
using Library.Shared.Models.FileStorage.Dtos;

namespace FileStorage.API.Application.Services
{
    public class FileInformationService : IFileInformationService
    {
        private readonly IFolderInformationRepository _folderInformationRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public FileInformationService(IFolderInformationRepository folderInformationRepository,
            IMapper mapper,
            ILogger logger)
        {
            _folderInformationRepository = folderInformationRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<FileInformationDto> PutFileInformationAsync(PutFileCommand command)
        {
            var folderInformation = await GetOrCreateFolderInformationFromDatabaseAsync(command);

            var fileInformation = FileInformation.CreateDefault(command.Name, folderInformation);

            folderInformation.AddOrReplaceFileInformation(fileInformation);
            _logger.Info(
                $"New file #{fileInformation.FileInformationId} with key '{fileInformation.Key}' added to the folder #{folderInformation.FolderInformationId} with key '{folderInformation.Key}'");

            await _folderInformationRepository.UpsertFolderInformationAsync(
                _mapper.Map<FolderInformationPersistenceModel>(folderInformation));
            _logger.Info(
                $"Folder #{folderInformation.FolderInformationId} with key '{folderInformation.Key}' upserted in the database. Current files count: {folderInformation.FileInformations.Count}");

            return _mapper.Map<FileInformationDto>(_mapper.Map<FileInformation>(fileInformation));
        }

        private async Task<FolderInformation> GetOrCreateFolderInformationFromDatabaseAsync(PutFileCommand command)
        {
            var folderInformation = FolderInformation.CreateDefault(command.Key);

            var folderInformationPersistenceModel = await _folderInformationRepository.GetFolderInformationByKeyAsync(folderInformation.Key);

            if (folderInformationPersistenceModel is not null)
            {
                _logger.Trace($"Folder #{folderInformationPersistenceModel.FolderInformationId} with key '{folderInformation.Key}' found in the database. Updating entry");
                folderInformation = _mapper.Map<FolderInformation>(folderInformationPersistenceModel);
                folderInformation.UpdateNow();
            }

            _logger.Trace($"Folder with key '{folderInformation.Key}' not found in the database. Inserting entry");

            return folderInformation;
        }
    }
}