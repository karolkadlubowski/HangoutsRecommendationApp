using System.Threading;
using System.Threading.Tasks;
using AccountDefinition.API.Application.Abstractions;
using AutoMapper;
using Library.Database.Transaction.Abstractions;
using Library.EventBus;
using Library.Shared.Events.Abstractions;
using Library.Shared.Logging;
using Library.Shared.Models.AccountDefinition.Dtos;
using MediatR;

namespace AccountDefinition.API.Application.Features.AddAccountProvider
{
    public class AddAccountProviderCommandHandler : IRequestHandler<AddAccountProviderCommand, AddAccountProviderResponse>
    {
        private readonly IAccountProviderService _accountProviderService;
        private readonly IEventSender _eventSender;
        private readonly ITransactionManager _transactionManager;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public AddAccountProviderCommandHandler(IAccountProviderService accountProviderService,
            IEventSender eventSender,
            ITransactionManager transactionManager,
            IMapper mapper,
            ILogger logger)
        {
            _accountProviderService = accountProviderService;
            _eventSender = eventSender;
            _transactionManager = transactionManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AddAccountProviderResponse> Handle(AddAccountProviderCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = _transactionManager.CreateScope())
            {
                _logger.Trace("> Database transaction began");

                var addedAccountProvider = await _accountProviderService.AddAccountProviderAsync(request);

                await _eventSender.SendEventAsync(EventBusTopics.AccountDefinition,
                    addedAccountProvider.FirstStoredEvent,
                    cancellationToken);

                transaction.Complete();

                _logger.Trace("< Database transaction committed");

                return new AddAccountProviderResponse { AddedAccountProvider = _mapper.Map<AccountProviderDto>(addedAccountProvider) };
            }
        }
    }
}