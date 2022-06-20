using MediatR;

namespace Identity.API.Application.Features.ChangeUserPassword
{
    public record ChangeUserPasswordCommand(string Password) : IRequest<ChangeUserPasswordResponse>;
}