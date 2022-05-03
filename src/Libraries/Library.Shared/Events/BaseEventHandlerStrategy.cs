using System.Threading;
using System.Threading.Tasks;
using Library.EventBus;
using Library.EventBus.Transaction;
using Library.Shared.Events.Abstractions;
using MediatR;

namespace Library.Shared.Events
{
    public abstract class BaseEventHandlerStrategy : IEventHandlerStrategy
    {
        protected readonly IMediator _mediator;

        protected BaseEventHandlerStrategy(IMediator mediator)
            => _mediator = mediator;

        public abstract EventType EventType { get; }

        public abstract Task<DistributedTransactionResponse> HandleEventAsync(Event @event, CancellationToken cancellationToken = default);
    }
}