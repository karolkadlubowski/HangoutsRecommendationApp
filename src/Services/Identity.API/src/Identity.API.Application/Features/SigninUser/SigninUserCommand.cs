using MediatR;

namespace Identity.API.Application.Features.SigninUser
{
    public record SignInUserCommand
    (
        string Email,
        string Password
    ) : IRequest<SignInUserResponse>;
}