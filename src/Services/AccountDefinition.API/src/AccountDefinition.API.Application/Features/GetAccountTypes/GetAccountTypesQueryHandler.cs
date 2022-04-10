using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AccountDefinition.API.Application.Database.Repositories;
using AutoMapper;
using Library.Shared.Logging;
using Library.Shared.Models.AccountDefinition.Dtos;
using MediatR;

namespace AccountDefinition.API.Application.Features.GetAccountTypes
{
    public class GetAccountTypesQueryHandler : IRequestHandler<GetAccountTypesQuery, GetAccountTypesResponse>
    {
        private readonly IAccountTypeRepository _accountTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetAccountTypesQueryHandler(IAccountTypeRepository accountTypeRepository,
            IMapper mapper,
            ILogger logger)
        {
            _accountTypeRepository = accountTypeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetAccountTypesResponse> Handle(GetAccountTypesQuery request, CancellationToken cancellationToken)
        {
            var accountTypesFromDb = await _accountTypeRepository.GetAccountTypesAsync();

            _logger.Info($"{accountTypesFromDb.Count} account types fetched from the database");

            return new GetAccountTypesResponse
            {
                AccountTypes = _mapper.Map<IReadOnlyList<AccountTypeDto>>(accountTypesFromDb)
            };
        }
    }
}