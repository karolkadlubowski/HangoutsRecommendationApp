using System.Threading;
using System.Threading.Tasks;
using Library.EventBus;
using Library.Shared.Models.Identity.Events.DataModels;
using MediatR;
using UserProfile.API.Application.Abstractions;
using UserProfile.API.Application.Handlers.UpdateEmailAddress;

namespace UserProfile.API.Application.Handlers.Strategies
{
    public class UserEmailChangedEventHandlerStrategy : IEventHandlerStrategy
    {
        private readonly IMediator _mediator;

        public UserEmailChangedEventHandlerStrategy(IMediator mediator)
            => _mediator = mediator;

        public async Task HandleEventAsync(Event @event, CancellationToken cancellationToken = default)
        {
            var dataModel = @event.GetData<UserEmailChangedEventDataModel>();

            await _mediator.Send(new UpdateEmailAddressCommand(dataModel.UserId,
                    dataModel.CurrentEmailAddress),
                cancellationToken);
        }
    }
}