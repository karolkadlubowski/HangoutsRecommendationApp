using System;
using System.Collections.Generic;
using System.Linq;
using Library.Shared.Events.Abstractions;
using Library.Shared.Logging;

namespace Library.Shared.Events
{
    public class DefaultEventAggregatorCacheCleaner : IEventAggregatorCacheCleaner
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILogger _logger;

        public DefaultEventAggregatorCacheCleaner(IEventAggregator eventAggregator,
            ILogger logger)
        {
            _eventAggregator = eventAggregator;
            _logger = logger;
        }

        public IReadOnlyList<Guid> CleanUpCache(int cacheExpirationTimeInMinutes)
        {
            var cleanedUpTransactionsIds = new List<Guid>();

            for (var index = 0; index < _eventAggregator.EventsTransactions.Keys.Count; index++)
            {
                var transactionId = _eventAggregator.EventsTransactions.Keys.ElementAt(index);
                var eventsInTransaction = _eventAggregator.EventsTransactions[transactionId];

                if (!eventsInTransaction.Any())
                    continue;

                var lastEventCreatedOn = eventsInTransaction.LastOrDefault()?.CreatedOn;

                if (!lastEventCreatedOn.HasValue)
                    continue;

                if (lastEventCreatedOn.Value.AddMinutes(cacheExpirationTimeInMinutes) < DateTime.UtcNow)
                {
                    _eventAggregator.EventsTransactions.Remove(transactionId, out _);
                    cleanedUpTransactionsIds.Add(transactionId);
                }
            }

            _logger.Info($"Events transactions with IDs: [{string.Join(",", cleanedUpTransactionsIds)}] cleaned up from the memory cache");
            return cleanedUpTransactionsIds;
        }
    }
}