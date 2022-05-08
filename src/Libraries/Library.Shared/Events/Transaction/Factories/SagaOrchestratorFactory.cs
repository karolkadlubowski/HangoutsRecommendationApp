using System.Collections.Generic;
using System.Linq;
using Library.Shared.Events.Transaction.Abstractions;
using Library.Shared.Exceptions;

namespace Library.Shared.Events.Transaction.Factories
{
    public class SagaOrchestratorFactory : ISagaOrchestratorFactory
    {
        private readonly IEnumerable<ISagaOrchestrator> _sagaOrchestrators;

        public SagaOrchestratorFactory(IEnumerable<ISagaOrchestrator> sagaOrchestrators)
            => _sagaOrchestrators = sagaOrchestrators;

        public ISagaOrchestrator CreateSagaOrchestrator<TSagaOrchestrator>() where TSagaOrchestrator : ISagaOrchestrator
            => _sagaOrchestrators.SingleOrDefault(so => so.GetType() == typeof(TSagaOrchestrator))
               ?? throw new ServerException($"Saga orchestrator of type: '{nameof(TSagaOrchestrator)}' not defined");
    }
}