using MediatR;

namespace UserProfile.API.Application.Features.GetUserProfileQuery
{
    public record GetUserProfileQuery : IRequest<GetUserProfileResponse>
    {
        public long UserId { get; init; }
    }
}