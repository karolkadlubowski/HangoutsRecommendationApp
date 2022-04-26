using System;
using System.Threading.Tasks;
using AutoMapper;
using Library.Shared.Logging;
using UserProfile.API.Application.Abstractions;
using UserProfile.API.Application.Database.PersistenceModels;
using UserProfile.API.Application.Database.Repositories;
using UserProfile.API.Application.Features.GetUserProfileQuery;
using UserProfile.API.Application.Handlers.UpdateEmailAddress;

namespace UserProfile.API.Application.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UserProfileService(IUserProfileRepository userProfileRepository, IMapper mapper, ILogger logger)
        {
            _userProfileRepository = userProfileRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Domain.Entities.UserProfile> GetUserProfileAsync(GetUserProfileQuery request)
        {
            //await _userProfileRepository.UpdateUserProfileAsync(request.UserId, new UserProfilePersistenceModel { UserId = 1, EmailAddress = "misio@gmail.com" });
            var userProfilePersistenceModel = await _userProfileRepository.GetUserProfileAsync(request.UserId);
            //TODO Implement downloading from IdentityApi if response is null
            var userProfile = _mapper.Map<Domain.Entities.UserProfile>(userProfilePersistenceModel);
            _logger.Info($"User profile #{userProfile.UserId} fetched from the database");

            return userProfile;
        }

        public async Task<Domain.Entities.UserProfile> UpdateUserProfileAsync(UpdateEmailAddressCommand command)
        {
            var userProfile = Domain.Entities.UserProfile.Create(command.EmailAddress);
            _logger.Info($"User profile #{command.UserId} updated in database");
            //return _userProfileRepository.UpdateUserProfileAsync(request.UserId, _mapper.Map<UserProfilePersistenceModel>(request));
            await _userProfileRepository.UpdateUserProfileAsync(command.UserId,_mapper.Map<UserProfilePersistenceModel>(userProfile));
            return userProfile;
        }
    }
}