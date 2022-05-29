using MediatR;

namespace VenueReview.API.Application.Features.DeleteVenueReview
{
    public record DeleteVenueReviewCommand : IRequest<DeleteVenueReviewResponse>
    {
        public string VenueReviewId { get; init; }
    }
}