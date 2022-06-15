using MediatR;

namespace VenueReview.API.Application.Handlers.DeleteReviewsFromVenue
{
    public record DeleteVenuesFromVenueCommand
    (
        long VenueId
    ) : IRequest<DeleteVenuesFromVenueResponse>;
}