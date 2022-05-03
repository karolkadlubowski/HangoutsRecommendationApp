using MediatR;

namespace UserProfile.API.Application.Handlers.UpdateEmailAddress
{
    public record UpdateEmailAddressCommand
    (
        long UserId,
        string EmailAddress
    ) : IRequest<UpdateEmailAddressResponse>;
}