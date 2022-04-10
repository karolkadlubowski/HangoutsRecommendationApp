using System;
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

        public async Task<TEvent> SendEventAsync<TEvent, TData>(string topic, TData data = default,
            CancellationToken cancellationToken = default)
            where TEvent : Event, new()
            where TData : class
        {
            var @event = EventFactory<TEvent>.CreateEvent(data);
            await _eventPublisher.PublishAsync(topic, @event, cancellationToken);

            _logger.Info(
                $">> Event #{@event.EventId} sent to the message broker topic: '{topic}' in transaction #{@event.TransactionId}");

            return @event;
        }

        public async Task<TEvent> SendEventWithoutDataAsync<TEvent>(string topic,
            CancellationToken cancellationToken = default)
            where TEvent : Event, new()
        {
            var @event = EventFactory<TEvent>.CreateEventWithoutData();
            await _eventPublisher.PublishAsync(topic, @event, cancellationToken);

            _logger.Info(
                $">> Event #{@event.EventId} sent to the message broker topic: '{topic}' in transaction #{@event.TransactionId}");

            return @event;
        }

        public async Task<TEvent> SendEventInTransactionAsync<TEvent, TData>(string topic, TData data,
            Guid transactionId,
            CancellationToken cancellationToken = default)
            where TEvent : Event, new()
            where TData : class
        {
            var @event = EventFactory<TEvent>.CreateEventInTransaction(transactionId, data);
            await _eventPublisher.PublishAsync(topic, @event, cancellationToken);

            _logger.Info(
                $">> Event #{@event.EventId} sent to the message broker topic: '{topic}' in transaction #{@event.TransactionId}");

            return @event;
        }
    }
}