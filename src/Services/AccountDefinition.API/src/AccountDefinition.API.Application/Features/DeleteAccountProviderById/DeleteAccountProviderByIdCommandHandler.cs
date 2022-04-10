using System.Threading;
using System.Threading.Tasks;
using AccountDefinition.API.Application.Abstractions;
using Library.Database.Transaction.Abstractions;
using Library.EventBus;
using Library.Shared.Events.Abstractions;
using Library.Shared.Logging;
using Library.Shared.Models.AccountDefinition.Events;
using MediatR;

namespace AccountDefinition.API.Application.Features.DeleteAccountProviderById
{
    public class DeleteAccountProviderByIdCommandHandler : IRequestHandler<DeleteAccountProviderByIdCommand, DeleteAccountProviderByIdResponse>
    {
        private readonly IAccountProviderService _accountProviderService;
        private readonly IEventSender _eventSender;
        private readonly ITransactionManager _transactionManager;
        private readonly ILogger _logger;

        public DeleteAccountProviderByIdCommandHandler(IAccountProviderService accountProviderService,
            IEventSender eventSender,
            ITransactionManager transactionManager,
            ILogger logger)
        {
            _accountProviderService = accountProviderService;
            _eventSender = eventSender;
            _transactionManager = transactionManager;
            _logger = logger;
        }

        public async Task<DeleteAccountProviderByIdResponse> Handle(DeleteAccountProviderByIdCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = _transactionManager.CreateScope())
            {
                _logger.Trace("> Database transaction began");

                var deletedAccountProviderId = await _accountProviderService.DeleteAccountProviderByIdAsync(request);

                await _eventSender.SendEventWithoutDataAsync<AccountProviderDeletedEvent>(EventBusTopics.AccountDefinition, cancellationToken);

                transaction.Complete();

                _logger.Trace("< Database transaction committed");

                return new DeleteAccountProviderByIdResponse
                {
                    DeletedAccountProviderId = deletedAccountProviderId
                };
            }
        }
    }
}