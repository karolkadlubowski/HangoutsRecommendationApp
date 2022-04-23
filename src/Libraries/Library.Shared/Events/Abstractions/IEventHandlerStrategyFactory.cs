using Library.EventBus;

namespace Library.Shared.Events.Abstractions
{
    public interface IEventHandlerStrategyFactory
    {
        IEventHandlerStrategy CreateStrategy(Event @event);
    }
}