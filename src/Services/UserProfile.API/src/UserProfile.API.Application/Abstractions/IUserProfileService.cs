using System.Threading.Tasks;
using UserProfile.API.Application.Handlers.UpdateEmailAddress;

namespace UserProfile.API.Application.Abstractions
{
    public interface IUserProfileService : IReadOnlyUserProfileService
    {
        Task<Domain.Entities.UserProfile> UpdateUserProfileAsync(UpdateEmailAddressCommand command);
    }
}