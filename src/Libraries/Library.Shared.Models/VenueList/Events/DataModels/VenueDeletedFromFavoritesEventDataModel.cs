namespace Library.Shared.Models.VenueList.Events.DataModels
{
    public record VenueDeletedFromFavoritesEventDataModel
    {
        public long VenueId { get; init; }
        public long UserId { get; init; }
    }
}