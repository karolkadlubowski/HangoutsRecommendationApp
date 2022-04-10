using System.Threading.Tasks;
using AccountDefinition.API.Application.Abstractions;
using AccountDefinition.API.Application.Database.Repositories;
using AccountDefinition.API.Application.Features.AddAccountProvider;
using AutoMapper;
using Library.Shared.Exceptions;
using Library.Shared.Logging;
using Library.Shared.Models.AccountDefinition.Dtos;

namespace AccountDefinition.API.Infrastructure.Services
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
                $"New account provider #{accountProvider.AccountProviderId} of type: '{accountProvider.Provider}' inserted to the database successfully");

            return _mapper.Map<AccountProviderDto>(accountProvider);
        }
    }
}