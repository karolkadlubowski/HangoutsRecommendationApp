namespace VenueList.API.Application.Handlers.DeleteFavoritesFromVenue
{
    public record DeleteFavoritesFromVenueResponse
    {
        public long VenueId { get; init; }

        public DeleteFavoritesFromVenueResponse()
        {
        }
    }
}