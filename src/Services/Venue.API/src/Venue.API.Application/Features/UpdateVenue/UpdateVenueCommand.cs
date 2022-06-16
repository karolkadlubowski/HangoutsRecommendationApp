using Library.Shared.Models.Venue.Enums;
using MediatR;

namespace Venue.API.Application.Features.UpdateVenue
{
    public record UpdateVenueCommand : IRequest<UpdateVenueResponse>
    {
        public long VenueId { get; init; }

        public string VenueName { get; init; }
        public string Description { get; init; }
        public string CategoryName { get; init; }

        public string Address { get; init; }

        public double Latitude { get; init; }
        public double Longitude { get; init; }

        public VenueStyle Style { get; init; }
        public VenueOccupancy Occupancy { get; init; }
    }
}