using System.Threading.Tasks;
using AutoMapper;
using Library.EventBus;
using Library.Shared.Exceptions;
using Library.Shared.Logging;
using Library.Shared.Models.Venue.Enums;
using Library.Shared.Models.Venue.Events;
using Library.Shared.Models.Venue.Events.DataModels;
using Venue.API.Application.Abstractions;
using Venue.API.Application.Database;
using Venue.API.Application.Database.PersistenceModels;
using Venue.API.Application.Features.CreateVenue;
using Venue.API.Domain.Entities.Builders;

namespace Venue.API.Application.Services
{
    public class VenueLocationService : IVenueLocationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public VenueLocationService(IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Domain.Entities.Venue> CreateVenueWithoutLocationAsync(CreateVenueCommand command, string categoryId, long creatorId)
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

        public async Task<Domain.Entities.Venue> DeleteLocationFromVenueAsync(long venueId)
        {
            var venuePersistenceModel = await _unitOfWork.VenueRepository.FindByIdAsync(venueId)
                                        ?? throw new EntityNotFoundException($"Venue #{venueId} not found in the database");

            _logger.Info($"Venue #{venuePersistenceModel.VenueId} with persist state '{venuePersistenceModel.PersistState}' and location #{venuePersistenceModel.LocationId} found in the database");

            venuePersistenceModel.PersistState = VenuePersistState.PersistedWithoutLocation;

            _unitOfWork.VenueRepository.Update(venuePersistenceModel);

            if (!await _unitOfWork.CompleteAsync())
                throw new DatabaseOperationException($"Venue #{venuePersistenceModel.VenueId} persist state cannot be changed to '{VenuePersistState.PersistedWithoutLocation}' in the database");

            var venue = _mapper.Map<Domain.Entities.Venue>(venuePersistenceModel);

            _logger.Info($"Venue #{venue.VenueId} persist state changed to '{VenuePersistState.PersistedWithoutLocation}' successfully");

            venue.AddDomainEvent(EventFactory<VenueLocationDeletedEvent>.CreateEvent(venue.VenueId,
                new VenueLocationDeletedEventDataModel { VenueId = venue.VenueId, LocationId = venue.LocationId.GetValueOrDefault() }));

            return venue;
        }
    }
}