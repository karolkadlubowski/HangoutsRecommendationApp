using System;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Shared.Events.Transaction.Abstractions
{
    public interface ISagaOrchestrator
    {
        Task<DistributedTransactionResult> OrchestrateTransactionAsync(Guid transactionId, Guid firstEventId,
            CancellationToken cancellationToken = default);
    }
}