using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Library.EventBus;
using Library.Shared.Exceptions;
using Library.Shared.Logging;
using Library.Shared.Models.Pagination;
using Library.Shared.Models.Pagination.Models;
using Library.Shared.Models.Venue.Enums;
using Library.Shared.Models.Venue.Events;
using Library.Shared.Models.Venue.Events.DataModels;
using Venue.API.Application.Abstractions;
using Venue.API.Application.Database;
using Venue.API.Application.Database.PersistenceModels;
using Venue.API.Application.Features.CreateVenue;
using Venue.API.Application.Features.GetVenue;
using Venue.API.Application.Features.GetVenues;
using Venue.API.Domain.Entities.Builders;
using Venue.API.Domain.Entities.Models;

namespace Venue.API.Application.Services
{
    public class VenueService : IVenueService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageDataService _fileStorageDataService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public VenueService(IUnitOfWork unitOfWork,
            IFileStorageDataService fileStorageDataService,
            IMapper mapper,
            ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _fileStorageDataService = fileStorageDataService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Domain.Entities.Venue> GetVenueWithPhotosAsync(GetVenueQuery query)
        {
            var venuePersistenceModel = await _unitOfWork.VenueRepository.FindByIdAsync(query.VenueId)
                                        ?? throw new EntityNotFoundException($"Venue #{query.VenueId} not found in the database");

            var photosFromApi = await _fileStorageDataService.GetPhotosFromFolderAsync(venuePersistenceModel.VenueId);

            var (venue, photos) = (_mapper.Map<Domain.Entities.Venue>(venuePersistenceModel),
                _mapper.Map<IReadOnlyList<Photo>>(photosFromApi));

            venue.AddPhotos(photos);

            _logger.Info($"Venue #{venue.VenueId} loaded from the database successfully. Venue photos count: {venue.Photos.Count}");

            return venue;
        }

        public async Task<PaginationTuple<Domain.Entities.Venue>> GetVenuesAsync(GetVenuesQuery query)
        {
            var venuesPersistenceModels = await _unitOfWork.VenueRepository.GetPaginatedVenuesAsync(query.PageNumber, query.PageSize);
            var pagination = PaginationResponseDecorator.Create(venuesPersistenceModels);

            _logger.Info($"Venues: {venuesPersistenceModels.CurrentPage} page with {venuesPersistenceModels.CurrentCount} records loaded from the database");

            return new PaginationTuple<Domain.Entities.Venue>(_mapper.Map<IReadOnlyList<Domain.Entities.Venue>>(venuesPersistenceModels), pagination);
        }

        public async Task<Domain.Entities.Venue> CreateVenueAsync(CreateVenueCommand command, string categoryId, long creatorId)
        {
            var venue = new VenueBuilder(command.Name, categoryId)
                .WithDescription(command.Description)
                .CreatedBy(creatorId)
                .Build();

            venue.UpdatePersistState(VenuePersistState.PersistedWithoutLocation);

            var venuePersistenceModel = _mapper.Map<VenuePersistenceModel>(venue);

            _unitOfWork.VenueRepository.Add(venuePersistenceModel);

            if (!await _unitOfWork.CompleteAsync())
                throw new DatabaseOperationException($"Inserting venue with name '{venuePersistenceModel.Name}' and persist state '{venuePersistenceModel.PersistState}' to the database failed");

            _logger.Info(
                $"Venue #{venuePersistenceModel.VenueId} with persist state '{venuePersistenceModel.PersistState}' inserted to the database successfully. Venue status: {venuePersistenceModel.Status}");

            venue = _mapper.Map<VenuePersistenceModel, Domain.Entities.Venue>(venuePersistenceModel);

            venue.AddDomainEvent(EventFactory<VenueCreatedWithoutLocationEvent>.CreateEvent(venue.VenueId,
                _mapper.Map<VenueCreatedWithoutLocationEventDataModel>(venue)));

            return venue;
        }
    }
}