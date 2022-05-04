namespace Library.Shared.Models.Location.Events.DataModels
{
    public record LocationDeletingFailedEventDataModel
    {
        public long LocationId { get; init; }
        public long VenueId { get; init; }
    }
}