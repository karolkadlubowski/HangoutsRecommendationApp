using System.Threading.Tasks;
using AutoMapper;
using Library.Shared.Exceptions;
using Library.Shared.Logging;
using VenueList.API.Application.Abstractions;
using VenueList.API.Application.Database.PersistenceModels;
using VenueList.API.Application.Database.Repositories;
using VenueList.API.Application.Features.AddVenue;
using VenueList.API.Domain.Entities;

namespace VenueList.API.Application.Services
{
    public class VenueService : IVenueService
    {
        private readonly IVenueRepository _venueRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public VenueService(
            IVenueRepository venueRepository,
            IMapper mapper,
            ILogger logger)
        {
            _venueRepository = venueRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Venue> AddVenueAsync(AddVenueCommand command)
        {
            var venue = Venue.Create(command.VenueId, command.UserId, command.Name, command.Description, command.CategoryId, command.CreatorId);

            var venuePersistenceModel = await _venueRepository.InsertVenueAsync(venue)
                                        ?? throw new DatabaseOperationException($"Inserting venue with id #{venue.VenueId} for a user with id #{venue.UserId} to the database failed");

            venue = _mapper.Map<VenuePersistenceModel, Domain.Entities.Venue>(venuePersistenceModel);

            _logger.Info($"Venue with id #{venue.VenueId} for a user with id #{venue.UserId} added to the database successfully");

            return venue;
        }
    }
}