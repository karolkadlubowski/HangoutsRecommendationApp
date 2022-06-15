using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Identity.API.Application.Database.Repositories;
using Identity.API.Domain.Entities;
using Library.EventBus;
using Library.Shared.Events.Abstractions;
using Library.Shared.Exceptions;
using Library.Shared.HttpAccessor;
using Library.Shared.Logging;
using Library.Shared.Models.Identity.Events;
using Library.Shared.Models.Identity.Events.DataModels;
using MediatR;

namespace Identity.API.Application.Features.ChangeUserEmail
{
    public class ChangeUserEmailCommandHandler : IRequestHandler<ChangeUserEmailCommand, ChangeUserEmailResponse>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IReadOnlyHttpAccessor _httpAccessor;
        private readonly IEventSender _eventSender;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ChangeUserEmailCommandHandler(IIdentityRepository identityRepository,
            IReadOnlyHttpAccessor httpAccessor,
            IEventSender eventSender,
            IMapper mapper,
            ILogger logger)
        {
            _identityRepository = identityRepository;
            _httpAccessor = httpAccessor;
            _eventSender = eventSender;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ChangeUserEmailResponse> Handle(ChangeUserEmailCommand request, CancellationToken cancellationToken)
        {
            var userPersistenceModel = await _identityRepository.FindUserAsync(_httpAccessor.CurrentUserId)
                                       ?? throw new EntityNotFoundException($"User #{_httpAccessor.CurrentUserId} not found");

            var user = _mapper.Map<User>(userPersistenceModel);

            if (await _identityRepository.AnyUserWithEmailExistsAsync(request.Email))
                throw new DuplicateExistsException($"User with {request.Email} already exists");

            user.SetEmail(request.Email);

            _mapper.Map(user, userPersistenceModel);

            if (!await _identityRepository.UpdateUserAsync(userPersistenceModel))
                throw new DatabaseOperationException($"Error while changing email for user #{user.UserId}");

            _logger.Info($"User #{user.UserId} password updated successfully");

            user.AddDomainEvent(EventFactory<UserEmailChangedEvent>.CreateEvent(user.UserId,
                _mapper.Map<UserEmailChangedEventDataModel>(user)));

            await _eventSender.SendEventAsync(EventBusTopics.Identity, user.FirstStoredEvent,
                cancellationToken);

            return new ChangeUserEmailResponse();
        }
    }
}