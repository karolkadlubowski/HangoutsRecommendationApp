using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Shared.Logging;
using Library.Shared.Models.AccountDefinition.Dtos;
using Library.Shared.Models.UserProfile;
using MediatR;
using UserProfile.API.Application.Database.PersistenceModels;
using UserProfile.API.Application.Database.Repositories;

namespace UserProfile.API.Application.Features.GetUserProfileQuery
{
    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, GetUserProfileResponse>
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public GetUserProfileQueryHandler(IUserProfileRepository userProfileRepository, IMapper mapper, ILogger logger)
        {
            _userProfileRepository = userProfileRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetUserProfileResponse> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            // await _userProfileRepository.UpdateUserProfileAsync(request.UserId, new UserProfilePersistenceModel { UserId = 1, EmailAddress = "misio@gmail.com" });
            var userProfile = await _userProfileRepository.GetUserProfileAsync(request.UserId);
            _logger.Info($"User profile #{userProfile.UserId} fetched from the database");

            return new GetUserProfileResponse()
            {
                UserProfile = _mapper.Map<UserProfileDto>(userProfile)
            };
            //return new GetUserProfileResponse{ UserProfile = new UserProfileDto{UserId = userProfile.UserId, EmailAddress = userProfile.EmailAddress}};
        }
    }
}