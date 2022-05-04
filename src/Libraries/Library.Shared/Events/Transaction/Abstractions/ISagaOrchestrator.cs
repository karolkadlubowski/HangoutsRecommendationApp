using System.Threading;
using System.Threading.Tasks;
using Library.EventBus;

namespace Library.Shared.Events.Transaction.Abstractions
{
    public interface ISagaOrchestrator
    {
        Task<DistributedTransactionResult> OrchestrateTransactionAsync(Event firstEvent,
            CancellationToken cancellationToken = default);
    }
}