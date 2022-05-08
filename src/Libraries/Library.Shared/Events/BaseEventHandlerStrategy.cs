using System.Threading;
using System.Threading.Tasks;
using Library.EventBus;
using Library.Shared.Events.Abstractions;
using Library.Shared.Events.Transaction;
using MediatR;

namespace Library.Shared.Events
{
    public abstract class BaseEventHandlerStrategy : IEventHandlerStrategy
    {
        protected readonly IMediator _mediator;

        protected BaseEventHandlerStrategy(IMediator mediator)
            => _mediator = mediator;

        public abstract EventType EventType { get; }

        public abstract Task<DistributedTransactionResult> HandleEventAsync(Event @event, CancellationToken cancellationToken = default);
    }
}