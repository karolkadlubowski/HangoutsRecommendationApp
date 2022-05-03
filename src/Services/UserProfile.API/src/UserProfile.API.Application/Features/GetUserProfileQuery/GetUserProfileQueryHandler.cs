using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Shared.Models.UserProfile;
using MediatR;
using UserProfile.API.Application.Abstractions;

namespace UserProfile.API.Application.Features.GetUserProfileQuery
{
    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, GetUserProfileResponse>
    {
        private readonly IReadOnlyUserProfileService _userProfileService;
        private readonly IMapper _mapper;

        public GetUserProfileQueryHandler(IReadOnlyUserProfileService userProfileService, IMapper mapper)
        {
            _userProfileService = userProfileService;
            _mapper = mapper;
        }

        public async Task<GetUserProfileResponse> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
            => new GetUserProfileResponse()
            {
                UserProfile = _mapper.Map<UserProfileDto>(await _userProfileService.GetUserProfileAsync(request))
            };
    }
}