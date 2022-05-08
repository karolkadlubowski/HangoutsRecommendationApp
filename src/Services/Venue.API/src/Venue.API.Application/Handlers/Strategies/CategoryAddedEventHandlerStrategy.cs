using System.Threading;
using System.Threading.Tasks;
using Library.EventBus;
using Library.Shared.Events;
using Library.Shared.Events.Transaction;
using Library.Shared.Models.Category.Events.DataModels;
using MediatR;
using Venue.API.Application.Handlers.AddCategoryToCache;

namespace Venue.API.Application.Handlers.Strategies
{
    public class CategoryAddedEventHandlerStrategy : BaseEventHandlerStrategy
    {
        public CategoryAddedEventHandlerStrategy(IMediator mediator) : base(mediator)
        {
        }

        public override EventType EventType => EventType.CATEGORY_ADDED;

        public override async Task<DistributedTransactionResult> HandleEventAsync(Event @event, CancellationToken cancellationToken = default)
        {
            var dataModel = @event.GetData<CategoryAddedEventDataModel>();

            await _mediator.Send(new AddCategoryToCacheCommand(dataModel.CategoryId, dataModel.Name));

            return DistributedTransactionResult.Default(@event.TransactionId, @event.EventId);
        }
    }
}