using System.Threading;
using System.Threading.Tasks;
using Library.Shared.Logging;
using MediatR;
using VenueReview.API.Application.Database.Repositories;

namespace VenueReview.API.Application.Handlers.DeleteReviewsFromVenue
{
    public class DeleteReviewsFromVenueCommandHandler : IRequestHandler<DeleteVenuesFromVenueCommand, DeleteVenuesFromVenueResponse>
    {
        private readonly IVenueReviewRepository _venueReviewRepository;
        private readonly ILogger _logger;

        public DeleteReviewsFromVenueCommandHandler(IVenueReviewRepository venueReviewRepository, ILogger logger)
        {
            _venueReviewRepository = venueReviewRepository;
            _logger = logger;
        }

        public async Task<DeleteVenuesFromVenueResponse> Handle(DeleteVenuesFromVenueCommand request, CancellationToken cancellationToken)
        {
            await _venueReviewRepository.DeleteVenueReviewsByVenueIdAsync(request.VenueId);

            _logger.Info($"All venue reviews were successfully deleted for venue with id #{request.VenueId}");

            return new DeleteVenuesFromVenueResponse() {VenueId = request.VenueId};
        }
    }
}