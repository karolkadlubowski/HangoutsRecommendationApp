using System.Threading;
using System.Threading.Tasks;
using AccountDefinition.API.Application.Abstractions;
using AccountDefinition.API.Application.Features.GetAccountTypes;
using MediatR;

namespace AccountDefinition.API.Application.Features.GetAccountProviders
{
    public class GetAccountProvidersQueryHandler : IRequestHandler<GetAccountProvidersQuery, GetAccountProvidersResponse>
    {
        private readonly IReadOnlyAccountProviderService _accountProviderService;
        
        public GetAccountProvidersQueryHandler(IReadOnlyAccountProviderService accountProviderService)
        {
            _accountProviderService = accountProviderService;
        }

        public async Task<GetAccountProvidersResponse> Handle(GetAccountProvidersQuery request, CancellationToken cancellationToken)
            => new GetAccountProvidersResponse()
            {
                AccountProviders = await _accountProviderService.GetAccountProvidersAsync()
            };
    }
}