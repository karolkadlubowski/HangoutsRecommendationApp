using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Database.Transaction.Abstractions;
using Library.Shared.Events.Abstractions;
using Library.Shared.Logging;
using MediatR;
using UserProfile.API.Application.Abstractions;

namespace UserProfile.API.Application.Handlers.UpdateEmailAddress
{
    public class UpdateEmailAddressCommandHandler : IRequestHandler<UpdateEmailAddressCommand,UpdateEmailAddressResponse>
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;


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