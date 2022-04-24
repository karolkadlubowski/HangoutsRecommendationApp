using Library.Shared.Models.Venue.Dtos;

namespace Library.Shared.Models.Venue.Events.DataModels
{
    public record VenueCreatedWithoutLocationEventDataModel
    {
        public VenueDto CreatedVenue { get; init; }
    }
}