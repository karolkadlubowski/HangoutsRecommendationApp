using Library.Shared.Models.Venue.Dtos;
using Library.Shared.Models.Venue.Enums;

namespace Library.Shared.Models.Venue.Events.DataModels
{
    public record VenueCreatedEventDataModel
    {
        public long VenueId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string CategoryId { get; init; }
        public long? CreatorId { get; init; }
        public VenueStatus Status { get; init; }
        public VenueStyle Style { get; init; }
        public VenueOccupancy Occupancy { get; init; }

        public LocationDto Location { get; init; }
    }
}