using Library.EventBus;

namespace Library.Shared.Models.VenueList.Events
{
    public record VenueDeletedFromFavoritesEvent : Event
    {
        public VenueDeletedFromFavoritesEvent() => EventType = EventType.VENUE_DELETED_FROM_FAVORITES;
    }
}