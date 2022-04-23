using Library.EventBus;
using UserProfile.API.Application.Abstractions;

namespace UserProfile.API.Application.Handlers.Strategies.Factories
{
    public interface IEventHandlerStrategyFactory
    {
        IEventHandlerStrategy CreateStrategy(Event @event);
    }
}