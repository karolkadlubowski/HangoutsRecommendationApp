using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AccountDefinition.API.Application.Abstractions;
using AutoMapper;
using Library.Shared.Models.AccountDefinition.Dtos;
using MediatR;

namespace AccountDefinition.API.Application.Features.GetAccountTypes
{
    public class GetAccountTypesQueryHandler : IRequestHandler<GetAccountTypesQuery, GetAccountTypesResponse>
    {
        private readonly IReadOnlyAccountTypeService _accountTypeService;
        private readonly IMapper _mapper;

        public GetAccountTypesQueryHandler(IReadOnlyAccountTypeService accountTypeService,
            IMapper mapper)
        {
            _accountTypeService = accountTypeService;
            _mapper = mapper;
        }

        public async Task<GetAccountTypesResponse> Handle(GetAccountTypesQuery request, CancellationToken cancellationToken)
            => new GetAccountTypesResponse { AccountTypes = _mapper.Map<IReadOnlyList<AccountTypeDto>>(await _accountTypeService.GetAccountTypesAsync()) };
    }
}