using System;
using System.Threading;
using System.Threading.Tasks;
using Library.EventBus;
using Library.EventBus.Abstractions;
using Library.Shared.Constants;
using Library.Shared.Events.Abstractions;
using Microsoft.Extensions.Hosting;
using NLog;
using ILogger = Library.Shared.Logging.ILogger;

namespace UserProfile.API.Infrastructure.HostedServices
{
    public class EventConsumerHostedService : IHostedService
    {
        private readonly IEventConsumer _eventConsumer;
        private readonly IEventAggregator _eventAggregator;
        private readonly ILogger _logger;

        public EventConsumerHostedService(IEventConsumer eventConsumer,
            IEventAggregator eventAggregator,
            ILogger logger)
        {
            _eventConsumer = eventConsumer;
            _eventAggregator = eventAggregator;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                using (MappedDiagnosticsLogicalContext.SetScoped(LoggingConstants.Scope,
                           LoggingConstants.GetScopeValue($"{nameof(EventConsumerHostedService)}")))
                {
                    _logger.Info($"{nameof(EventConsumerHostedService)} hosted service started. Events consuming and aggregating started");

                    await _eventConsumer.ConsumeFromLatestAsync(EventBusTopics.Category, cancellationToken);
                    _logger.Info($"> Consuming from the message broker topic: '{EventBusTopics.Identity}'");

                    await _eventAggregator.AggregateEventsAsync(cancellationToken);
                }
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
            => await Task.Run(()
                => _logger.Info($"{nameof(EventConsumerHostedService)} hosted service stopped. Events consuming and aggregating stopped"));
    }
}