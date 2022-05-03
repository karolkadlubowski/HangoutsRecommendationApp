using System.Threading;
using System.Threading.Tasks;
using Library.Shared.Logging;
using MediatR;
using UserProfile.API.Application.Abstractions;

namespace UserProfile.API.Application.Handlers.UpdateEmailAddress
{
    public class UpdateEmailAddressCommandHandler : IRequestHandler<UpdateEmailAddressCommand, UpdateEmailAddressResponse>
    {
        private readonly IUserProfileService _userProfileService;

        public UpdateEmailAddressCommandHandler(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        public async Task<UpdateEmailAddressResponse> Handle(UpdateEmailAddressCommand request, CancellationToken cancellationToken)
        {
            var updatedUserProfile = await _userProfileService.UpdateUserProfileAsync(request);
            
            return new UpdateEmailAddressResponse
            {
                UserId = updatedUserProfile.UserId,
                CurrentEmailAddress = updatedUserProfile.EmailAddress
            };
        }
    }
}