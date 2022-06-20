using MediatR;

namespace Identity.API.Application.Features.SignupUser
{
    public record SignupUserCommand
    (
        string Email,
        string Password
    ) : IRequest<SignupUserResponse>;
}