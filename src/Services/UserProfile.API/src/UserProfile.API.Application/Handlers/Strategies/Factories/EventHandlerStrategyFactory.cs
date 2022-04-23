using Library.EventBus;
using Library.Shared.Events.Abstractions;
using Library.Shared.Exceptions;
using MediatR;

namespace UserProfile.API.Application.Handlers.Strategies.Factories
{
    public class EventHandlerStrategyFactory : IEventHandlerStrategyFactory
    {
        private readonly IMediator _mediator;

        public EventHandlerStrategyFactory(IMediator mediator)
            => _mediator = mediator;

        public IEventHandlerStrategy CreateStrategy(Event @event)
            => @event.EventType switch
            {
                EventType.USER_EMAIL_CHANGED => new UserEmailChangedEventHandlerStrategy(_mediator),
                _ => throw new ServerException($"Event of type '{@event.EventType}' not recognized")
            };
    }
}