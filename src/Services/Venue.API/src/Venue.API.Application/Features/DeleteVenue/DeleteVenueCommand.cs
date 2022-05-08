using MediatR;

namespace Venue.API.Application.Features.DeleteVenue
{
    public record DeleteVenueCommand : IRequest<DeleteVenueResponse>
    {
        public long VenueId { get; init; }
    }
}