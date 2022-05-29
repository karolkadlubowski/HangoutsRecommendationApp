using MediatR;

namespace VenueReview.API.Application.Features.AddVenueReview
{
    public record AddVenueReviewCommand : IRequest<AddVenueReviewResponse>
    {
        public long VenueId { get; init; }
        public string Content { get; init; }
        public long CreatorId { get; init; }
        public double Rating { get; init; }
    }
}