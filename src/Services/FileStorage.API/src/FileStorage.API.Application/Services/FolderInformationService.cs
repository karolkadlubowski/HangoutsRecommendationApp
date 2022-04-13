using System.Threading.Tasks;
using AutoMapper;
using FileStorage.API.Application.Abstractions;
using FileStorage.API.Application.Database.Repositories;
using FileStorage.API.Application.Features.GetFolderByKey;
using FileStorage.API.Domain.Entities;
using FileStorage.API.Domain.ValueObjects;
using Library.Shared.Exceptions;
using Library.Shared.Logging;
using Library.Shared.Models.FileStorage.Dtos;

namespace FileStorage.API.Application.Services
{
    public class FolderInformationService : IFolderInformationService
    {
        private readonly IFolderInformationRepository _folderInformationRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public FolderInformationService(IFolderInformationRepository folderInformationRepository,
            IMapper mapper,
            ILogger logger)
        {
            _folderInformationRepository = folderInformationRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<FolderInformationDto> GetFolderByKeyAsync(GetFolderByKeyQuery query)
        {
            var folderKey = new FolderInformationKey(query.Key);

            var folderInformationPersistenceModel = await _folderInformationRepository.GetFolderInformationByKeyAsync(folderKey)
                                         ?? throw new EntityNotFoundException($"Folder with key: '{folderKey.Value}' not found in the database");

            var folderInformation = _mapper.Map<FolderInformation>(folderInformationPersistenceModel);

            _logger.Info($"Folder #{folderInformation.FolderInformationId} with key: '{folderInformation.Key}' found in the database. It contains {folderInformation.FileInformations.Count} files");

            return _mapper.Map<FolderInformationDto>(folderInformation);
        }
    }
}