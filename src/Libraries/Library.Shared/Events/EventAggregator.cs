using System;
using System.Threading;
using System.Threading.Tasks;
using Library.EventBus;
using Library.EventBus.Abstractions;
using Library.Shared.Constants;
using Library.Shared.DI;
using Library.Shared.Events.Abstractions;
using Library.Shared.Events.Transaction;
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

        public event EventHandler<DistributedTransactionResult> TransactionUpdated;

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

                        if (eventHandlerStrategy is null)
                        {
                            _logger.Warning($"Event handler strategy of type '{receivedEvent.EventType}' not defined");
                            return;
                        }

                        _logger.Trace($"Event handler strategy of type '{receivedEvent.EventType}' found");

                        var result = await eventHandlerStrategy.HandleEventAsync(receivedEvent);
                        _logger.Info($"<< Event #{receivedEvent.EventId} of type '{receivedEvent.EventType}' consumed");

                        if (result is not null)
                            TransactionUpdated?.Invoke(this, result);
                    }
                }
                catch (Exception e)
                {
                    _logger.Error(e.Message, e);
                }
            }
        }
    }
}