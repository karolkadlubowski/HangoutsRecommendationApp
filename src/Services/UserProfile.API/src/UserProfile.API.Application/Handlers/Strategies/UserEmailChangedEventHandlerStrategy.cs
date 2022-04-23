using System.Threading;
using System.Threading.Tasks;
using Library.EventBus;
using Library.Shared.Events;
using Library.Shared.Models.Identity.Events.DataModels;
using MediatR;
using UserProfile.API.Application.Handlers.UpdateEmailAddress;

namespace UserProfile.API.Application.Handlers.Strategies
{
    public class UserEmailChangedEventHandlerStrategy : BaseEventHandlerStrategy
    {
        public UserEmailChangedEventHandlerStrategy(IMediator mediator) : base(mediator)
        {
        }

        public async override Task HandleEventAsync(Event @event, CancellationToken cancellationToken = default)
        {
            var dataModel = @event.GetData<UserEmailChangedEventDataModel>();

            await _mediator.Send(new UpdateEmailAddressCommand(dataModel.UserId,
                    dataModel.CurrentEmailAddress),
                cancellationToken);
        }
    }
}