using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Library.Shared.Logging;
using Venue.API.Application.Abstractions;
using Venue.API.Application.Database;
using Venue.API.Application.Features.GetVenuesByLocationsIds;

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

        public async Task<IReadOnlyList<Domain.Entities.Venue>> GetVenuesByLocationsIdsAsync(GetVenuesByLocationsIdsQuery query)
        {
            throw new System.NotImplementedException();
        }
    }
}