using Library.Shared.Models.Response;
using Library.Shared.Models.UserProfile;

namespace UserProfile.API.Application.Features.GetUserProfileQuery
{
    public record GetUserProfileResponse : BaseResponse
    {
        public UserProfileDto UserProfile { get; init; }

        public GetUserProfileResponse(Error error = null) : base(error)
        {
        }
    }
}