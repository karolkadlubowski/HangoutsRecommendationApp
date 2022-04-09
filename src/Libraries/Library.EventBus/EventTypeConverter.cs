namespace Library.EventBus
{
    public static class EventTypeConverter
    {
        public static string Convert(EventType eventType)
            => eventType.ToString().ToUpperInvariant();
    }
}