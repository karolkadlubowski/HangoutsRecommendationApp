namespace Library.EventBus
{
    public static class EventTypeConverter
    {
        public static string Convert(this EventType eventType)
            => eventType.ToString().ToUpperInvariant();
    }
}