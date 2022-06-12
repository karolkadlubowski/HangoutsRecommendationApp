using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Identity.API.Application.Abstractions;
using Identity.API.Application.Database.Repositories;
using Identity.API.Domain.Entities;
using Library.Shared.Exceptions;
using Library.Shared.Logging;
using MediatR;

namespace Identity.API.Application.Features.SigninUser
{
    public class SigninUserCommandHandler : IRequestHandler<SigninUserCommand, SigninUserResponse>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IPasswordHashService _passwordHashService;
        private readonly IAuthTokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public SigninUserCommandHandler(IIdentityRepository identityRepository,
            IPasswordHashService passwordHashService,
            IAuthTokenService tokenService,
            IMapper mapper,
            ILogger logger)
        {
            _identityRepository = identityRepository;
            _passwordHashService = passwordHashService;
            _tokenService = tokenService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<SigninUserResponse> Handle(SigninUserCommand request, CancellationToken cancellationToken)
        {
            var userPersistenceModel = await _identityRepository.FindUserAsync(request.Email)
                                       ?? throw new InvalidCredentialsException("Invalid email or password");

            if (!_passwordHashService.VerifyPasswordHash(request.Password,
                    userPersistenceModel.PasswordSalt,
                    userPersistenceModel.PasswordHash))
                throw new InvalidCredentialsException("Invalid email or password");

            var user = _mapper.Map<User>(userPersistenceModel);
            var jwtToken = _tokenService.GenerateAuthenticationToken(user);

            return new SigninUserResponse
            {
                JwtToken = jwtToken
            };
        }
    }
}