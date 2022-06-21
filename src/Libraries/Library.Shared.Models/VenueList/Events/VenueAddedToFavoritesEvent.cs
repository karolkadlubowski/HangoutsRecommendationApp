using Library.EventBus;

namespace Library.Shared.Models.VenueList.Events
{
    public record VenueAddedToFavoritesEvent : Event
    {
        public VenueAddedToFavoritesEvent() => EventType = EventType.VENUE_ADDED_TO_FAVORITES;
    }
}