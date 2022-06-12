using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Identity.API.Application.Abstractions;
using Identity.API.Application.Database.PersistenceModels;
using Identity.API.Application.Database.Repositories;
using Identity.API.Domain.Entities;
using Library.Shared.Exceptions;
using Library.Shared.Logging;
using MediatR;

namespace Identity.API.Application.Features.SignupUser
{
    public class SignupUserCommandHandler : IRequestHandler<SignupUserCommand, SignupUserResponse>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IPasswordHashService _passwordHashService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public SignupUserCommandHandler(IIdentityRepository identityRepository,
            IPasswordHashService passwordHashService,
            IMapper mapper,
            ILogger logger)
        {
            _identityRepository = identityRepository;
            _passwordHashService = passwordHashService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<SignupUserResponse> Handle(SignupUserCommand request, CancellationToken cancellationToken)
        {
            if (await _identityRepository.AnyUserWithEmailAsync(request.Email))
                throw new DuplicateExistsException("Error while creating user");

            var passwordSalt = _passwordHashService.CreatePasswordSalt();
            var passwordHash = _passwordHashService.HashPassword(request.Password, passwordSalt);

            var user = new User(request.Email, passwordSalt, passwordHash);

            var userPersistanceModel = _mapper.Map<UserPersistenceModel>(user);

            if (!await _identityRepository.AddUserAsync(userPersistanceModel))
                throw new DatabaseOperationException($"Adding new user with email '{request.Email}' to database failed");

            _logger.Info($"User with email '{user.Email}' created");

            return new SignupUserResponse();
        }
    }
}