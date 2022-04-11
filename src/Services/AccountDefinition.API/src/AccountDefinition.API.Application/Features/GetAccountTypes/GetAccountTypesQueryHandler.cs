using System.Threading;
using System.Threading.Tasks;
using AccountDefinition.API.Application.Abstractions;
using MediatR;

namespace AccountDefinition.API.Application.Features.GetAccountTypes
{
    public class GetAccountTypesQueryHandler : IRequestHandler<GetAccountTypesQuery, GetAccountTypesResponse>
    {
        private readonly IReadOnlyAccountTypeService _accountTypeService;

        public GetAccountTypesQueryHandler(IReadOnlyAccountTypeService accountTypeService)
        {
            _accountTypeService = accountTypeService;
        }

        public async Task<GetAccountTypesResponse> Handle(GetAccountTypesQuery request, CancellationToken cancellationToken)
            => new GetAccountTypesResponse
            {
                AccountTypes = await _accountTypeService.GetAccountTypesAsync()
            };
    }
}