using Library.Shared.Models.Response;

namespace Identity.API.Application.Features.SigninUser
{
    public record SignInUserResponse : BaseResponse
    {
        public string JwtToken { get; init; }

        public SignInUserResponse(Error error = null) : base(error)
        {
        }
    }
}