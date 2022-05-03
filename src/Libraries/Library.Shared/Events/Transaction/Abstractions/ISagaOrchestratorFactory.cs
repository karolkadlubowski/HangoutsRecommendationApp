namespace Library.Shared.Events.Transaction.Abstractions
{
    public interface ISagaOrchestratorFactory
    {
        ISagaOrchestrator CreateSagaOrchestrator<TSagaOrchestrator>()
            where TSagaOrchestrator : ISagaOrchestrator;
    }
}