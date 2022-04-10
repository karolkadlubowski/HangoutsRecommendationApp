using System.Threading.Tasks;
using AccountDefinition.API.Application.Abstractions;
using AccountDefinition.API.Application.Database.Repositories;
using AccountDefinition.API.Application.Features.AddAccountProvider;
using AccountDefinition.API.Application.Features.DeleteAccountProviderById;
using AutoMapper;
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

        public async Task<AccountProviderDto> AddAccountProviderAsync(AddAccountProviderCommand command)
        {
            var accountProvider = await _accountProviderRepository.InsertAccountProviderAsync(command.Provider)
                                  ?? throw new DatabaseOperationException(
                                      $"Inserting account provider of type: '{command.Provider}' to the database failed");

            _logger.Info(
                $"Account provider #{accountProvider.AccountProviderId} of type: '{accountProvider.Provider}' inserted to the database successfully");

            return _mapper.Map<AccountProviderDto>(accountProvider);
        }

        public async Task<long> DeleteAccountProviderByIdAsync(DeleteAccountProviderByIdCommand byIdCommand)
        {
            var deletedAccountProviderId = await _accountProviderRepository.DeleteAccountProviderByIdAsync(byIdCommand.AccountProviderId);

            if (deletedAccountProviderId == default)
                throw new EntityNotFoundException($"Account provider #{byIdCommand.AccountProviderId} not found in the database");

            _logger.Info($"Account provider #{deletedAccountProviderId} deleted from the database successfully");

            return deletedAccountProviderId;
        }
    }
}