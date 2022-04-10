using System.Threading;
using System.Threading.Tasks;
using AccountDefinition.API.Application.Abstractions;
using Library.EventBus;
using Library.Shared.Events.Abstractions;
using Library.Shared.Models.AccountDefinition.Events;
using MediatR;

namespace AccountDefinition.API.Application.Features.AddAccountProvider
{
    public class AddAccountProviderCommandHandler : IRequestHandler<AddAccountProviderCommand, AddAccountProviderResponse>
    {
        private readonly IAccountProviderService _accountProviderService;
        private readonly IEventSender _eventSender;

        public AddAccountProviderCommandHandler(IAccountProviderService accountProviderService,
            IEventSender eventSender)
        {
            _accountProviderService = accountProviderService;
            _eventSender = eventSender;
        }

        public async Task<AddAccountProviderResponse> Handle(AddAccountProviderCommand request, CancellationToken cancellationToken)
        {
            var addedAccountProvider = await _accountProviderService.AddAccountProviderAsync(request);

            await _eventSender.SendEventWithoutDataAsync<AccountProviderAddedEvent>(EventBusTopics.AccountDefinition, cancellationToken);

            return new AddAccountProviderResponse { AddedAccountProvider = addedAccountProvider, };
        }
    }
}