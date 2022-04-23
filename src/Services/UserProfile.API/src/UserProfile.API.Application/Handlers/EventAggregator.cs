using System;
using System.Threading;
using System.Threading.Tasks;
using Library.EventBus;
using Library.EventBus.Abstractions;
using Library.Shared.Logging;
using UserProfile.API.Application.Abstractions;
using UserProfile.API.Application.Handlers.Strategies.Factories;

namespace UserProfile.API.Application.Handlers
{
    public class EventAggregator : IEventAggregator
    {
        private readonly IEventHandlerStrategyFactory _eventHandlerStrategyFactory;
        private readonly IEventConsumer _eventConsumer;
        private readonly ILogger _logger;

        public EventAggregator(IEventHandlerStrategyFactory eventHandlerStrategyFactory,
            IEventConsumer eventConsumer,
            ILogger logger)
        {
            _eventHandlerStrategyFactory = eventHandlerStrategyFactory;
            _eventConsumer = eventConsumer;
            _logger = logger;
        }

        public async Task AggregateEventsAsync(CancellationToken cancellationToken = default)
            => await Task.Run(() => _eventConsumer.EventReceived += (_, receivedEvent)
                => Task.Run(async () => await HandleEventAsync(receivedEvent)));

        private async Task HandleEventAsync(Event receivedEvent)
        {
            try
            {
                _logger.Info($">> Event #{receivedEvent.EventId} of type '{receivedEvent.EventType}' for entity #{receivedEvent.EntityId} in transaction #{receivedEvent.TransactionId} received");

                var eventHandlerStrategy = _eventHandlerStrategyFactory.CreateStrategy(receivedEvent);
                _logger.Trace($"Event handler strategy of type '{receivedEvent.EventType}' found");

                await eventHandlerStrategy.HandleEventAsync(receivedEvent);
                _logger.Info($"<< Event #{receivedEvent.EventId} of type '{receivedEvent.EventType}' for entity #{receivedEvent.EntityId} in transaction #{receivedEvent.TransactionId} consumed");
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
            }
        }
    }
}