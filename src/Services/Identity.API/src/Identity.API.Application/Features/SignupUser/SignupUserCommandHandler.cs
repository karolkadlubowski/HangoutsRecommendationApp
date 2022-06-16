using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Identity.API.Application.Abstractions;
using Identity.API.Application.Database.PersistenceModels;
using Identity.API.Application.Database.Repositories;
using Identity.API.Domain.Entities;
using Library.EventBus;
using Library.Shared.Events.Abstractions;
using Library.Shared.Exceptions;
using Library.Shared.Logging;
using Library.Shared.Models.Identity.Events;
using Library.Shared.Models.Identity.Events.DataModels;
using MediatR;

namespace Identity.API.Application.Features.SignupUser
{
    public class SignupUserCommandHandler : IRequestHandler<SignupUserCommand, SignupUserResponse>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IPasswordHashService _passwordHashService;
        private readonly IEventSender _eventSender;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public SignupUserCommandHandler(IIdentityRepository identityRepository,
            IPasswordHashService passwordHashService,
            IEventSender eventSender,
            IMapper mapper,
            ILogger logger)
        {
            _identityRepository = identityRepository;
            _passwordHashService = passwordHashService;
            _eventSender = eventSender;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<SignupUserResponse> Handle(SignupUserCommand request, CancellationToken cancellationToken)
        {
            if (await _identityRepository.AnyUserWithEmailExistsAsync(request.Email))
                throw new DuplicateExistsException("Error while creating user");

            var passwordSalt = _passwordHashService.CreatePasswordSalt();
            var passwordHash = _passwordHashService.HashPassword(request.Password, passwordSalt);

            var user = new User(request.Email, passwordSalt, passwordHash);

            var userPersistanceModel = _mapper.Map<UserPersistenceModel>(user);

            if (!await _identityRepository.AddUserAsync(userPersistanceModel))
                throw new DatabaseOperationException($"Adding new user with email '{request.Email}' to database failed");

            user.AddDomainEvent(EventFactory<UserCreatedEvent>.CreateEvent(user.UserId,
                _mapper.Map<UserCreatedEventDataModel>(user)));

            await _eventSender.SendEventAsync(EventBusTopics.Identity, user.FirstStoredEvent,
                cancellationToken);
            
            _logger.Info($"User with email '{user.Email}' created");
            
            return new SignupUserResponse();
        }
    }
}