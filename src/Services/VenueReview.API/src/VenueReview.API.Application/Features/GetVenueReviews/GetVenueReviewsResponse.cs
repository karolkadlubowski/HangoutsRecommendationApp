using System.Collections.Generic;
using System.Collections.Immutable;
using Library.Shared.Models.Response;
using Library.Shared.Models.VenueReview.Dtos;

namespace VenueReview.API.Application.Features
{
    public record GetVenueReviewsResponse : BaseResponse
    {
        public IReadOnlyList<VenueReviewDto> VenueReviews { get; init; } = ImmutableList<VenueReviewDto>.Empty;

        public GetVenueReviewsResponse(Error error = null) : base(error)
        {
            
        }
    }
}