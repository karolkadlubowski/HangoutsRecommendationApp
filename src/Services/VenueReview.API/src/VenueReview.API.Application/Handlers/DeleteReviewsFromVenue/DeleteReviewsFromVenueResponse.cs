using Library.Shared.Models.Response;

namespace VenueReview.API.Application.Handlers.DeleteReviewsFromVenue
{
    public record DeleteVenuesFromVenueResponse : BaseResponse
    {
        public long VenueId { get; init; }

        public DeleteVenuesFromVenueResponse(Error error = null) : base(error)
        {
        }
    }
}