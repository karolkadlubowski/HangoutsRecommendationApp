using System.Threading.Tasks;
using UserProfile.API.Application.Features.GetUserProfileQuery;

namespace UserProfile.API.Application.Abstractions
{
    public interface IReadOnlyUserProfileService
    {
        Task<Domain.Entities.UserProfile> GetUserProfileAsync(GetUserProfileQuery request);
    }
}