using System.Threading.Tasks;
using AutoMapper;
using Library.EventBus;
using Library.Shared.Exceptions;
using Library.Shared.Logging;
using Library.Shared.Models.Venue.Events;
using Library.Shared.Models.Venue.Events.DataModels;
using Venue.API.Application.Abstractions;
using Venue.API.Application.Database;
using Venue.API.Application.Database.PersistenceModels;
using Venue.API.Application.Features.CreateVenue;
using Venue.API.Application.Features.DeleteVenue;
using Venue.API.Domain.Entities.Builders;

namespace Venue.API.Application.Services
{
    public class VenueService : IVenueService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public VenueService(IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Domain.Entities.Venue> CreateVenueAsync(CreateVenueCommand command, string categoryId, long creatorId)
        {
            var venue = new VenueBuilder(command.VenueName, categoryId)
                .WithDescription(command.Description)
                .CreatedBy(creatorId)
                .WithLocation(command.Address, command.Latitude, command.Longitude)
                .Build();

            var venuePersistenceModel = _mapper.Map<VenuePersistenceModel>(venue);

            _unitOfWork.VenueRepository.Add(venuePersistenceModel);

            if (!await _unitOfWork.CompleteAsync())
                throw new DatabaseOperationException($"Inserting venue with name '{venuePersistenceModel.Name}' and address '{venuePersistenceModel.Location.Address}' to the database failed");

            _logger.Info(
                $"Venue #{venuePersistenceModel.VenueId} with location #{venuePersistenceModel.LocationId} and status '{venuePersistenceModel.Status}' inserted to the database successfully");

            venue = _mapper.Map<VenuePersistenceModel, Domain.Entities.Venue>(venuePersistenceModel);

            venue.AddDomainEvent(EventFactory<VenueCreatedEvent>.CreateEvent(venue.VenueId,
                _mapper.Map<VenueCreatedEventDataModel>(venue)));

            return venue;
        }

        public async Task<Domain.Entities.Venue> DeleteVenueAsync(DeleteVenueCommand command)
        {
            var venuePersistenceModel = await _unitOfWork.VenueRepository.FindVenueDetailsAsync(command.VenueId)
                                        ?? throw new EntityNotFoundException($"Venue #{command.VenueId} not found in the database");

            _logger.Info($"Venue #{venuePersistenceModel.VenueId} with status '{venuePersistenceModel.Status}' and location #{venuePersistenceModel.LocationId} found in the database");

            _unitOfWork.LocationRepository.Delete(venuePersistenceModel.Location);

            if (!await _unitOfWork.CompleteAsync())
                throw new DatabaseOperationException($"Venue #{venuePersistenceModel.VenueId} cannot be deleted from the database");

            var venue = _mapper.Map<Domain.Entities.Venue>(venuePersistenceModel);

            _logger.Info($"Venue #{venue.VenueId} with location #{venue.LocationId} deleted from the database successfully");

            venue.AddDomainEvent(EventFactory<VenueDeletedEvent>.CreateEvent(venue.VenueId,
                new VenueDeletedEventDataModel { VenueId = venue.VenueId, LocationId = venue.LocationId }));

            return venue;
        }
    }
}