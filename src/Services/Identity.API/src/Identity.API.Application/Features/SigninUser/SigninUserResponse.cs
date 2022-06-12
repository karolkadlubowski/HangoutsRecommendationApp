using Library.Shared.Models.Response;

namespace Identity.API.Application.Features.SigninUser
{
    public record SigninUserResponse : BaseResponse
    {
        public string JwtToken { get; init; }

        public SigninUserResponse(Error error = null) : base(error)
        {
        }
    }
}