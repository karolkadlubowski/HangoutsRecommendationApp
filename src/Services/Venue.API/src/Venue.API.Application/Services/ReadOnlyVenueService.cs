using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Library.Shared.Exceptions;
using Library.Shared.Logging;
using Library.Shared.Models.Pagination;
using Library.Shared.Models.Pagination.Models;
using Venue.API.Application.Abstractions;
using Venue.API.Application.Database;
using Venue.API.Application.Features.GetVenue;
using Venue.API.Application.Features.GetVenues;
using Venue.API.Domain.Entities.Models;

namespace Venue.API.Application.Services
{
    public class ReadOnlyVenueService : IReadOnlyVenueService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageDataService _fileStorageDataService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ReadOnlyVenueService(IUnitOfWork unitOfWork,
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
            var venuePersistenceModel = await _unitOfWork.VenueRepository.FindVenueWithDetailsAsync(query.VenueId)
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
            var venuesPersistenceModels = await _unitOfWork.VenueRepository.GetPaginatedVenuesAsync(query);
            var pagination = PaginationResponseDecorator.Create(venuesPersistenceModels);

            _logger.Info($"Venues: {venuesPersistenceModels.CurrentPage} page with {venuesPersistenceModels.CurrentCount} records loaded from the database");

            return new PaginationTuple<Domain.Entities.Venue>(_mapper.Map<IReadOnlyList<Domain.Entities.Venue>>(venuesPersistenceModels), pagination);
        }
    }
}