using System;
using System.Threading;
using System.Threading.Tasks;
using Library.EventBus;
using Library.EventBus.Abstractions;
using Library.Shared.Constants;
using Microsoft.Extensions.Hosting;
using NLog;
using ILogger = Library.Shared.Logging.ILogger;

namespace Venue.API.Infrastructure.HostedServices
{
    public class EventConsumerHostedService : IHostedService
    {
        private readonly IEventConsumer _eventConsumer;
        private readonly ILogger _logger;

        public EventConsumerHostedService(IEventConsumer eventConsumer,
            ILogger logger)
        {
            _eventConsumer = eventConsumer;
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

                    await _eventConsumer.ConsumeFromLatestAsync(EventBusTopics.Location, cancellationToken);
                    _logger.Info($"> Consuming from the message broker topic: '{EventBusTopics.Location}'");
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