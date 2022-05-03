using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Library.EventBus;
using Library.EventBus.Abstractions;
using Library.Shared.Constants;
using Library.Shared.DI;
using Library.Shared.Events.Abstractions;
using NLog;
using ILogger = Library.Shared.Logging.ILogger;

namespace Library.Shared.Events
{
    public class EventAggregator : IEventAggregator
    {
        private readonly IEventConsumer _eventConsumer;
        private readonly IDIProvider _diProvider;
        private readonly ILogger _logger;

        public EventAggregator(IEventConsumer eventConsumer,
            IDIProvider diProvider,
            ILogger logger)
        {
            _eventConsumer = eventConsumer;
            _diProvider = diProvider;
            _logger = logger;
        }

        public ConcurrentDictionary<Guid, HashSet<Event>> EventsTransactions { get; } = new ConcurrentDictionary<Guid, HashSet<Event>>();

        public async Task AggregateEventsAsync(CancellationToken cancellationToken = default)
            => await Task.Run(() => _eventConsumer.EventReceived += (_, receivedEvent)
                => Task.Run(async () => await HandleEventAsync(receivedEvent)));

        private async Task HandleEventAsync(Event receivedEvent)
        {
            using (MappedDiagnosticsLogicalContext.SetScoped(LoggingConstants.Scope,
                       LoggingConstants.GetScopeValue($"EventID: {receivedEvent.EventId}",
                           $"EntityID: {receivedEvent.EntityId}",
                           $"TransactionID: {receivedEvent.TransactionId}",
                           $"{receivedEvent.EventType}")))
            {
                try
                {
                    using (var scope = _diProvider.CreateScope())
                    {
                        var eventHandlerStrategyFactory = scope.ResolveService<IEventHandlerStrategyFactory>();

                        _logger.Info($">> Event #{receivedEvent.EventId} of type '{receivedEvent.EventType}' received");

                        var eventHandlerStrategy = eventHandlerStrategyFactory.CreateStrategy(receivedEvent);
                        _logger.Trace($"Event handler strategy of type '{receivedEvent.EventType}' found");

                        await eventHandlerStrategy.HandleEventAsync(receivedEvent);
                        _logger.Info($"<< Event #{receivedEvent.EventId} of type '{receivedEvent.EventType}' consumed");

                        AddOrUpdateEventsTransactionsDictionary(receivedEvent);
                    }
                }
                catch (Exception e)
                {
                    _logger.Error(e.Message, e);
                }
            }
        }

        private void AddOrUpdateEventsTransactionsDictionary(Event receivedEvent)
        {
            if (EventsTransactions.TryGetValue(receivedEvent.TransactionId, out var events))
                events.Add(receivedEvent);
            else
                EventsTransactions[receivedEvent.TransactionId] = new HashSet<Event>(new[] { receivedEvent });

            _logger.Trace($"Event #{receivedEvent.EventId} in transaction #{receivedEvent.TransactionId} added to the memory cache dictionary");
        }
    }
}