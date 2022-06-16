using MediatR;

namespace Identity.API.Application.Features.SigninUser
{
    public record SigninUserCommand
    (
        string Email,
        string Password
    ) : IRequest<SigninUserResponse>;
}