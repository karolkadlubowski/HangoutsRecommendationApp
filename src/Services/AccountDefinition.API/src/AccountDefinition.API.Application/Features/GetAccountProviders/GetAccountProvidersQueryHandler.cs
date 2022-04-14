using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AccountDefinition.API.Application.Abstractions;
using AutoMapper;
using Library.Shared.Models.AccountDefinition.Dtos;
using MediatR;

namespace AccountDefinition.API.Application.Features.GetAccountProviders
{
    public class GetAccountProvidersQueryHandler : IRequestHandler<GetAccountProvidersQuery, GetAccountProvidersResponse>
    {
        private readonly IReadOnlyAccountProviderService _accountProviderService;
        private readonly IMapper _mapper;

        public GetAccountProvidersQueryHandler(IReadOnlyAccountProviderService accountProviderService, IMapper mapper)
        {
            _accountProviderService = accountProviderService;
            _mapper = mapper;
        }

        public async Task<GetAccountProvidersResponse> Handle(GetAccountProvidersQuery request, CancellationToken cancellationToken)
            => new GetAccountProvidersResponse()
            {
                AccountProviders = _mapper.Map<IReadOnlyList<AccountProviderDto>>(await _accountProviderService.GetAccountProvidersAsync())
            };
    }
}