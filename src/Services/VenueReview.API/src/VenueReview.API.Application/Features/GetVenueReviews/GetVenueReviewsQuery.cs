using System;
using MediatR;

namespace VenueReview.API.Application.Features
{
    public record GetVenueReviewsQuery : IRequest<GetVenueReviewsResponse>
    {
        public long VenueId { get; init; }
    }
}