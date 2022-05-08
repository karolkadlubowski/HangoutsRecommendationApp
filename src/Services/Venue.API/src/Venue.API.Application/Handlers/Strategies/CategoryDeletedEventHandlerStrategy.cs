using System.Threading;
using System.Threading.Tasks;
using Library.EventBus;
using Library.Shared.Events;
using Library.Shared.Events.Transaction;
using Library.Shared.Models.Category.Events.DataModels;
using MediatR;
using Venue.API.Application.Handlers.DeleteCategoryFromCache;

namespace Venue.API.Application.Handlers.Strategies
{
    public class CategoryDeletedEventHandlerStrategy : BaseEventHandlerStrategy
    {
        public CategoryDeletedEventHandlerStrategy(IMediator mediator) : base(mediator)
        {
        }

        public override EventType EventType => EventType.CATEGORY_DELETED;

        public override async Task<DistributedTransactionResult> HandleEventAsync(Event @event, CancellationToken cancellationToken = default)
        {
            var dataModel = @event.GetData<CategoryDeletedEventDataModel>();

            await _mediator.Send(new DeleteCategoryFromCacheCommand(dataModel.CategoryId));

            return DistributedTransactionResult.Default(@event.TransactionId, @event.EventId);
        }
    }
}