using Library.Shared.Models.Response;

namespace VenueReview.API.Application.Features.DeleteVenueReview
{
    public record DeleteVenueReviewResponse : BaseResponse
    {
        public string DeletedVenueReviewId { get; init; }

        public DeleteVenueReviewResponse(Error error = null) : base(error)
        {
        }
    }
}