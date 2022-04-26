using System.Threading;
using System.Threading.Tasks;
using Library.EventBus;
using Library.Shared.Events;
using MediatR;
using UserProfile.API.Application.Handlers.UpdateEmailAddress;

namespace UserProfile.API.Application.Handlers.Strategies
{
    public class UserEmailChangedEventHandlerStrategy : BaseEventHandlerStrategy
    {
        public UserEmailChangedEventHandlerStrategy(IMediator mediator) : base(mediator)
        {
        }

        public override EventType EventType => EventType.CATEGORY_ADDED;

        public async override Task HandleEventAsync(Event @event, CancellationToken cancellationToken = default)
        {
            // var dataModel = @event.GetData<UserEmailChangedEventDataModel>();
            var dataModel = new { UserId = 1, CurrentEmailAddress = "kiwi@gmail.com" };

            await _mediator.Send(new UpdateEmailAddressCommand(dataModel.UserId,
                    dataModel.CurrentEmailAddress),
                cancellationToken);
        }
    }
}