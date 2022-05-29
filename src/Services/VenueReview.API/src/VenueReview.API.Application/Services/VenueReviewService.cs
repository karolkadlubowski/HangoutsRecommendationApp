using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Shared.Logging;
using VenueReview.API.Application.Database.Repositories;
using AutoMapper;
using Library.Shared.Exceptions;
using VenueReview.API.Application.Abstractions;
using VenueReview.API.Application.Features;

namespace VenueReview.API.Application.Services
{
    public class VenueReviewService : IVenueReviewService
    {
        private readonly IVenueReviewRepository _venueReviewRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public VenueReviewService(
            IVenueReviewRepository venueReviewRepository,
            IMapper mapper,
            ILogger logger
        )
        {
            _venueReviewRepository = venueReviewRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IReadOnlyList<Domain.Entities.VenueReview>> GetVenueReviewsAsync(GetVenueReviewsQuery query)
        {
            var venueReviewsPersistenceModel = await _venueReviewRepository.GetVenueReviewsAsync(query.VenueId)
                                              ?? throw new EntityNotFoundException($"VenueReviews with id:  '{query.VenueId} not found in the database");

            var venueReviews = _mapper.Map<IReadOnlyList<Domain.Entities.VenueReview>>(venueReviewsPersistenceModel);
            
            _logger.Info($"{venueReviews.Count} venueReviews read from the database");

            return venueReviews;
        }
    }
}