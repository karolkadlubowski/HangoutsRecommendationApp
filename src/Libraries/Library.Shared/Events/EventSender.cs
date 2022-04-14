using System.Threading;
using System.Threading.Tasks;
using Library.EventBus;
using Library.EventBus.Abstractions;
using Library.Shared.Events.Abstractions;
using Library.Shared.Logging;

namespace Library.Shared.Events
{
    public class EventSender : IEventSender
    {
        private readonly IEventPublisher _eventPublisher;
        private readonly ILogger _logger;

        public EventSender(IEventPublisher eventPublisher, ILogger logger)
        {
            _eventPublisher = eventPublisher;
            _logger = logger;
        }

        public async Task<Event> SendEventAsync(string topic, Event @event,
            CancellationToken cancellationToken = default)
        {
            await _eventPublisher.PublishAsync(topic, @event, cancellationToken);

            _logger.Info(
                $">> Event #{@event.EventId} with type '{@event.EventType}' sent to the message broker topic: '{topic}' in transaction #{@event.TransactionId}");

            return @event;
        }
    }
}