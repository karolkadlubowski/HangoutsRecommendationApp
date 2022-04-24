using System.Threading.Tasks;
using AutoMapper;
using Library.Shared.Logging;
using UserProfile.API.Application.Abstractions;
using UserProfile.API.Application.Database.Repositories;
using UserProfile.API.Application.Features.GetUserProfileQuery;

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

        public async Task<Domain.Entities.UserProfile> GetUserProfileAsync(GetUserProfileQuery query)
        {
            //await _userProfileRepository.UpdateUserProfileAsync(request.UserId, new UserProfilePersistenceModel { UserId = 1, EmailAddress = "misio@gmail.com" });
            var userProfilePersistenceModel = await _userProfileRepository.GetUserProfileAsync(query.UserId);
            //TODO Implement downloading from IdentityApi if response is null
            var userProfile = _mapper.Map<Domain.Entities.UserProfile>(userProfilePersistenceModel);
            _logger.Info($"User profile #{userProfile.UserId} fetched from the database");

            return userProfile;
        }
    }
}