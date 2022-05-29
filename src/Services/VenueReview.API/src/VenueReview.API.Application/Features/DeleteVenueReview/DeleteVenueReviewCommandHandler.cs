using System.Threading;
using System.Threading.Tasks;
using Library.EventBus;
using Library.Shared.Events.Abstractions;
using Library.Shared.Models.VenueReview.Events;
using Library.Shared.Models.VenueReview.Events.DataModels;
using MediatR;
using VenueReview.API.Application.Abstractions;

namespace VenueReview.API.Application.Features.DeleteVenueReview
{
    public class DeleteVenueReviewCommandHandler : IRequestHandler<DeleteVenueReviewCommand, DeleteVenueReviewResponse>
    {
        private readonly IVenueReviewService _venueReviewService;
        private readonly IEventSender _eventSender;

        public DeleteVenueReviewCommandHandler(IVenueReviewService venueReviewService,
            IEventSender eventSender)
        {
            _venueReviewService = venueReviewService;
            _eventSender = eventSender;
        }

        public async Task<DeleteVenueReviewResponse> Handle(DeleteVenueReviewCommand request, CancellationToken cancellationToken)
        {
            var deletedVenueReviewId = await _venueReviewService.DeleteVenueReviewAsync(request);

            await _eventSender.SendEventAsync(EventBusTopics.VenueReview,
                EventFactory<VenueReviewDeletedEvent>.CreateEvent(deletedVenueReviewId,
                    new VenueReviewDeletedEventDataModel {VenueReviewId = deletedVenueReviewId}),
                cancellationToken);

            return new DeleteVenueReviewResponse {DeletedVenueReviewId = deletedVenueReviewId};
        }
    }
}