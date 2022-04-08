namespace Library.EventBus
{
    public static class EventTypeConverter
    {
        public static string GetEventType(this EventType eventType)
            => eventType.ToString().ToUpperInvariant();
    }
}