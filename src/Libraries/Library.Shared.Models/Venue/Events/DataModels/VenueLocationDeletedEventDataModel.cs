namespace Library.Shared.Models.Venue.Events.DataModels
{
    public record VenueLocationDeletedEventDataModel
    {
        public long VenueId { get; init; }
        public long LocationId { get; init; }
    }
}