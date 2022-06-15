using System;
using Library.Shared.Models.Venue.Enums;

namespace Library.Shared.Models.Venue.Dtos
{
    public record VenueListDto
    {
        public long VenueId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string CategoryId { get; init; }
        public VenueStyle Style { get; init; }
        public VenueOccupancy Occupancy { get; init; }
        public long? CreatorId { get; init; }
        public VenueStatus Status { get; init; }
        public DateTime CreatedOn { get; init; }
        public DateTime? ModifiedOn { get; init; }

        public LocationDto Location { get; init; }
    }
}