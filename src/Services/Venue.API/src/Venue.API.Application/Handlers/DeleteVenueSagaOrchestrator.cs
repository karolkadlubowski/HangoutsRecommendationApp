﻿using System;
using System.Threading.Tasks;
using Library.Shared.Events.Abstractions;
using Library.Shared.Events.Transaction;
using Library.Shared.Logging;

namespace Venue.API.Application.Handlers
{
    public class DeleteVenueSagaOrchestrator : BaseSagaOrchestrator
    {
        public DeleteVenueSagaOrchestrator(IEventAggregator eventAggregator, ILogger logger)
            : base(eventAggregator, logger)
        {
        }

        protected override Task<DistributedTransactionResult> OrchestrateNextAsync(DistributedTransactionResult currentTransactionResult)
        {
            throw new NotImplementedException();
        }
    }
}