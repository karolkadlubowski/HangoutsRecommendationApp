using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Identity.API.Application.Abstractions;
using Identity.API.Application.Database.Repositories;
using Identity.API.Domain.Entities;
using Identity.API.Domain.ValueObjects;
using Library.Shared.Exceptions;
using Library.Shared.HttpAccessor;
using Library.Shared.Logging;
using MediatR;

namespace Identity.API.Application.Features.ChangeUserPassword
{
    public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, ChangeUserPasswordResponse>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IPasswordHashService _passwordHashService;
        private readonly IReadOnlyHttpAccessor _httpAccessor;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ChangeUserPasswordCommandHandler(IIdentityRepository identityRepository,
            IPasswordHashService passwordHashService,
            IReadOnlyHttpAccessor httpAccessor,
            IMapper mapper,
            ILogger logger)
        {
            _identityRepository = identityRepository;
            _passwordHashService = passwordHashService;
            _httpAccessor = httpAccessor;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ChangeUserPasswordResponse> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var userPersistenceModel = await _identityRepository.FindUserAsync(_httpAccessor.CurrentUserId)
                                       ?? throw new EntityNotFoundException($"User #{_httpAccessor.CurrentUserId} not found");

            var passwordSalt = _passwordHashService.CreatePasswordSalt();
            var passwordHash = _passwordHashService.HashPassword(new UserPassword(request.Password), passwordSalt);

            var user = _mapper.Map<User>(userPersistenceModel);

            user.SetPassword(passwordSalt, passwordHash);

            _mapper.Map(user, userPersistenceModel);

            if (!await _identityRepository.UpdateUserAsync(userPersistenceModel))
                throw new DatabaseOperationException($"Error while updating password for user #{user.UserId}");

            _logger.Info($"User #{user.UserId} password updated successfully");

            return new ChangeUserPasswordResponse();
        }
    }
}