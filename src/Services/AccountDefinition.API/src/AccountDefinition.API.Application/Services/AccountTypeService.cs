using System.Collections.Generic;
using System.Threading.Tasks;
using AccountDefinition.API.Application.Abstractions;
using AccountDefinition.API.Application.Database.Repositories;
using AutoMapper;
using Library.Shared.Logging;
using Library.Shared.Models.AccountDefinition.Dtos;

namespace AccountDefinition.API.Application.Services
{
    public class AccountTypeService : IReadOnlyAccountTypeService
    {
        private readonly IAccountTypeRepository _accountTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public AccountTypeService(IAccountTypeRepository accountTypeRepository,
            IMapper mapper,
            ILogger logger)
        {
            _accountTypeRepository = accountTypeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IReadOnlyList<AccountTypeDto>> GetAccountTypesAsync()
        {
            var accountTypesFromDb = await _accountTypeRepository.GetAccountTypesAsync();

            _logger.Info($"{accountTypesFromDb.Count} account types fetched from the database");

            return _mapper.Map<IReadOnlyList<AccountTypeDto>>(accountTypesFromDb);
        }
    }
}