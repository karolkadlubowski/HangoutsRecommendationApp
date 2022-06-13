using Library.Shared.Models.Response;
using Library.Shared.Models.VenueReview.Dtos;

namespace VenueReview.API.Application.Features.AddVenueReview
{
    public record AddVenueReviewResponse : BaseResponse
    {
        public VenueReviewDto AddedVenueReview { get; init; }

        public AddVenueReviewResponse(Error error = null) : base(error)
        {
        }
    }
}