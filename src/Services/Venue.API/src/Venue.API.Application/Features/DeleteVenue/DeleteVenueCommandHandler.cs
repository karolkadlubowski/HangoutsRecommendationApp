using System.Threading;
using System.Threading.Tasks;
using Library.Database.Transaction.Abstractions;
using Library.EventBus;
using Library.Shared.Events.Abstractions;
using Library.Shared.Events.Transaction.Abstractions;
using Library.Shared.Exceptions;
using Library.Shared.Logging;
using MediatR;
using Venue.API.Application.Abstractions;
using Venue.API.Application.Handlers;

namespace Venue.API.Application.Features.DeleteVenue
{
    public class DeleteVenueCommandHandler : IRequestHandler<DeleteVenueCommand, DeleteVenueResponse>
    {
        private readonly IVenueLocationService _venueLocationService;
        private readonly ITransactionManager _transactionManager;
        private readonly IEventSender _eventSender;
        private readonly ISagaOrchestrator _sagaOrchestrator;
        private readonly ILogger _logger;

        public DeleteVenueCommandHandler(IVenueLocationService venueLocationService,
            ITransactionManager transactionManager,
            IEventSender eventSender,
            ISagaOrchestratorFactory sagaOrchestratorFactory,
            ILogger logger)
        {
            _venueLocationService = venueLocationService;
            _transactionManager = transactionManager;
            _eventSender = eventSender;
            _sagaOrchestrator = sagaOrchestratorFactory.CreateSagaOrchestrator<DeleteVenueSagaOrchestrator>();
            _logger = logger;
        }

        public async Task<DeleteVenueResponse> Handle(DeleteVenueCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Venue venueWithoutLocation;

            using (var scope = _transactionManager.CreateScope())
            {
                _logger.Trace("> Database transaction began");

                venueWithoutLocation = await _venueLocationService.DeleteLocationFromVenueAsync(request);

                await _eventSender.SendEventAsync(EventBusTopics.Venue, venueWithoutLocation.FirstStoredEvent,
                    cancellationToken);

                scope.Complete();

                _logger.Trace("< Database transaction committed");
            }

            var distributedTransactionResult = await _sagaOrchestrator.OrchestrateTransactionAsync(venueWithoutLocation.FirstStoredEvent,
                cancellationToken);
            _logger.Info($"< Distributed transaction #{distributedTransactionResult.TransactionId} completed with the state: '{distributedTransactionResult.State}'");

            return distributedTransactionResult.IsCommitted
                ? new DeleteVenueResponse { DeletedVenueId = venueWithoutLocation.VenueId }
                : throw new ServerException($"Deleting venue #{request.VenueId} from the database failed. Distributed transaction state: '{distributedTransactionResult.State}'");
        }
    }
}