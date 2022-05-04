using System;
using System.Threading;
using System.Threading.Tasks;
using Library.EventBus;
using Library.Shared.Events;
using Library.Shared.Events.Transaction;
using Library.Shared.Logging;
using Library.Shared.Models.Venue.Events.DataModels;
using MediatR;
using Venue.API.Application.Handlers.RollbackVenueLocationDeleting;

namespace Venue.API.Application.Handlers.Strategies
{
    public class RollbackVenueLocationDeletingEventHandlerStrategy : BaseEventHandlerStrategy
    {
        private readonly ILogger _logger;

        public RollbackVenueLocationDeletingEventHandlerStrategy(IMediator mediator,
            ILogger logger) : base(mediator)
        {
            _logger = logger;
        }

        public override EventType EventType => EventType.VENUE_LOCATION_DELETING_ROLLBACKED;

        public async override Task<DistributedTransactionResult> HandleEventAsync(Event @event, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var dataModel = @event.GetData<VenueLocationDeletingRollbackedEventDataModel>();

                var result = await _mediator.Send(new RollbackVenueLocationDeletingCommand(@event,
                        dataModel.VenueId, dataModel.LocationId),
                    cancellationToken);

                return result;
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return DistributedTransactionResult.Default(@event.TransactionId, @event.EventId);
            }
        }
    }
}