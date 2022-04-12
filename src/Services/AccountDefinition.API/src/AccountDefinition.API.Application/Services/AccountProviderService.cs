using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using AccountDefinition.API.Application.Abstractions;
using AccountDefinition.API.Application.Database.PersistenceModels;
using AccountDefinition.API.Application.Database.Repositories;
using AccountDefinition.API.Application.Features.AddAccountProvider;
using AccountDefinition.API.Application.Features.DeleteAccountProviderById;
using AccountDefinition.API.Domain.Entities;
using AutoMapper;
using Library.Database.Extensions;
using Library.Shared.Exceptions;
using Library.Shared.Logging;
using Library.Shared.Models.AccountDefinition.Dtos;

namespace AccountDefinition.API.Application.Services
{
    public class AccountProviderService : IAccountProviderService
    {
        private readonly IAccountProviderRepository _accountProviderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public AccountProviderService(IAccountProviderRepository accountProviderRepository,
            IMapper mapper,
            ILogger logger)
        {
            _accountProviderRepository = accountProviderRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IReadOnlyList<AccountProviderDto>> GetAccountProvidersAsync()
        {
            var accountProviderPersistenceModels = await _accountProviderRepository.GetAccountProvidersAsync();

            var accountProviders = _mapper.Map<IReadOnlyList<AccountProvider>>(accountProviderPersistenceModels);
            
            _logger.Info($"{accountProviders.Count} account types fetched from the database");

            return _mapper.Map<IReadOnlyList<AccountProviderDto>>(accountProviders);
        }

        public async Task<AccountProviderDto> AddAccountProviderAsync(AddAccountProviderCommand command)
        {
            var accountProvider = AccountProvider.Create(command.Provider);

            try
            {
                var accountProviderPersistenceModel = await _accountProviderRepository.InsertAccountProviderAsync(accountProvider)
                                                      ?? throw new DatabaseOperationException(
                                                          $"Inserting account provider of type: '{accountProvider.Provider}' to the database failed");

                accountProvider = _mapper.Map<AccountProviderPersistenceModel, AccountProvider>(accountProviderPersistenceModel);

                _logger.Info(
                    $"Account provider #{accountProvider.AccountProviderId} of type: '{accountProvider.Provider}' inserted to the database successfully");

                return _mapper.Map<AccountProviderDto>(accountProvider);
            }
            catch (DbException e)
            {
                if (e.IsUniqueConstraintViolationException())
                    throw new DuplicateExistsException($"Account provider with provider name: '{accountProvider.Provider}' already exists in the database");

                throw;
            }
        }

        public async Task<long> DeleteAccountProviderByIdAsync(DeleteAccountProviderByIdCommand command)
        {
            var deletedAccountProviderId = await _accountProviderRepository.DeleteAccountProviderByIdAsync(command.AccountProviderId);

            if (deletedAccountProviderId == default)
                throw new EntityNotFoundException($"Account provider #{command.AccountProviderId} not found in the database");

            _logger.Info($"Account provider #{deletedAccountProviderId} deleted from the database successfully");

            return deletedAccountProviderId;
        }
    }
}