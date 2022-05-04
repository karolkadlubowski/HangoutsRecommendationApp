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
using Venue.API.Application.Features.DeleteVenue;
using Venue.API.Application.Handlers.RollbackVenueLocationDeleting;
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

        public async Task<Domain.Entities.Venue> DeleteLocationFromVenueAsync(DeleteVenueCommand command)
        {
            var venuePersistenceModel = await _unitOfWork.VenueRepository.FindByIdAsync(command.VenueId)
                                        ?? throw new EntityNotFoundException($"Venue #{command.VenueId} not found in the database");

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

        public async Task<Domain.Entities.Venue> RollbackLocationDeletingAsync(RollbackVenueLocationDeletingCommand command)
        {
            var venuePersistenceModel = await _unitOfWork.VenueRepository.FindByIdAsync(command.VenueId)
                                        ?? throw new EntityNotFoundException($"Venue #{command.VenueId} not found in the database");

            _logger.Info($"Venue #{venuePersistenceModel.VenueId} with persist state '{venuePersistenceModel.PersistState}' found in the database");

            venuePersistenceModel.LocationId = command.LocationId;
            venuePersistenceModel.PersistState = VenuePersistState.PersistedWithLocation;

            _unitOfWork.VenueRepository.Update(venuePersistenceModel);

            if (!await _unitOfWork.CompleteAsync())
                throw new DatabaseOperationException(
                    $"Venue #{venuePersistenceModel.VenueId} with location #{venuePersistenceModel.LocationId} persist state cannot be rollbacked to '{VenuePersistState.PersistedWithLocation}' in the database");

            var venue = _mapper.Map<VenuePersistenceModel, Domain.Entities.Venue>(venuePersistenceModel);

            _logger.Info($"Venue #{venue.VenueId} with location #{venue.LocationId} persist state rollbacked to '{VenuePersistState.PersistedWithLocation}' successfully");

            venue.AddDomainEvent(EventFactory<VenueLocationDeletingRollbackedEvent>.CreateEventInTransaction(command.Event.TransactionId,
                venue.VenueId,
                new VenueLocationDeletingRollbackedEventDataModel { VenueId = venue.VenueId, LocationId = venue.LocationId.GetValueOrDefault() }));

            return venue;
        }
    }
}