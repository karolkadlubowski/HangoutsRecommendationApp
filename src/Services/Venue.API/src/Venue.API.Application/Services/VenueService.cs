using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Library.Shared.Logging;
using Venue.API.Application.Abstractions;
using Venue.API.Application.Database;
using Venue.API.Application.Features.GetVenues;

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

        public async Task<IReadOnlyList<Domain.Entities.Venue>> GetVenuesAsync(GetVenuesQuery query)
        {
            var venuesPersistenceModels = await _unitOfWork.VenueRepository.GetPaginatedVenuesAsync(query.PageNumber, query.PageSize);

            _logger.Info($"Venues: {venuesPersistenceModels.CurrentPage} page loaded with {venuesPersistenceModels.Count()} records from the database");

            return _mapper.Map<IReadOnlyList<Domain.Entities.Venue>>(venuesPersistenceModels);
        }
    }
}