using System.Threading;
using System.Threading.Tasks;
using AccountDefinition.API.Application.Abstractions;
using MediatR;

namespace AccountDefinition.API.Application.Features.AddAccountProvider
{
    public class AddAccountProviderCommandHandler : IRequestHandler<AddAccountProviderCommand, AddAccountProviderResponse>
    {
        private readonly IAccountProviderService _accountProviderService;

        public AddAccountProviderCommandHandler(IAccountProviderService accountProviderService)
        {
            _accountProviderService = accountProviderService;
        }

        public async Task<AddAccountProviderResponse> Handle(AddAccountProviderCommand request, CancellationToken cancellationToken)
        {
            var addedAccountProvider = await _accountProviderService.AddAccountProviderAsync(request);

            return new AddAccountProviderResponse { AddedAccountProvider = addedAccountProvider };
        }
    }
}