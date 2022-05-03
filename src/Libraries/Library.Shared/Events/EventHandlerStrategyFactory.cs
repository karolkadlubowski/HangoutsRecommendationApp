using System.Collections.Generic;
using System.Linq;
using Library.EventBus;
using Library.Shared.Events.Abstractions;
using Library.Shared.Exceptions;

namespace Library.Shared.Events
{
    public class EventHandlerStrategyFactory : IEventHandlerStrategyFactory
    {
        private readonly IEnumerable<IEventHandlerStrategy> _eventHandlerStrategies;

        public EventHandlerStrategyFactory(IEnumerable<IEventHandlerStrategy> eventHandlerStrategies)
            => _eventHandlerStrategies = eventHandlerStrategies;

        public IEventHandlerStrategy CreateStrategy(Event @event)
            => _eventHandlerStrategies.SingleOrDefault(strategy => strategy.EventType == @event.EventType)
               ?? throw new ServerException($"Event of type '{@event.EventType}' not recognized");
    }
}