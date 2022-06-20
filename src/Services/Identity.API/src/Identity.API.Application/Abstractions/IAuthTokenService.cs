using Identity.API.Domain.Entities;

namespace Identity.API.Application.Abstractions
{
    public interface IAuthTokenService
    {
        string GenerateAuthenticationToken(User user);
    }
}