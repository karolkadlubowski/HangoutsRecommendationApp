using MediatR;

namespace Venue.API.Application.Features.GetVenue
{
    public record GetVenueQuery : IRequest<GetVenueResponse>
    {
        public long VenueId { get; init; }
    }
}