using MediatR;

namespace Identity.API.Application.Features.ChangeUserEmail
{
    public record ChangeUserEmailCommand(string Email) : IRequest<ChangeUserEmailResponse>;
}