namespace Library.EventBus
{
    public abstract record EventWithoutData : Event<object>
    {
        public EventWithoutData() => Data = null;
    }
}