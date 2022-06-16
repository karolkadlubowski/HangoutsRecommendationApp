using Library.Shared.Models.Response;

namespace Identity.API.Application.Features.SignupUser
{
    public record SignupUserResponse : BaseResponse
    {
        public SignupUserResponse(Error error = null) : base(error)
        {
        }
    }
}