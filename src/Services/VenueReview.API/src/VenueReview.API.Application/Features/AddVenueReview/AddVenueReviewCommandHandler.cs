using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.EventBus;
using Library.Shared.Events.Abstractions;
using Library.Shared.Models.VenueReview.Dtos;
using MediatR;
using VenueReview.API.Application.Abstractions;
using VenueReview.API.Application.Database.Repositories;

namespace VenueReview.API.Application.Features.AddVenueReview
{
    public class AddVenueReviewCommandHandler : IRequestHandler<AddVenueReviewCommand, AddVenueReviewResponse>
    {
        private readonly IVenueReviewService _venueReviewService;
        private readonly IEventSender _eventSender;
        private readonly IMapper _mapper;

        public AddVenueReviewCommandHandler(IVenueReviewService venueReviewService,
            IEventSender eventSender,
            IMapper mapper
        )
        {
            _venueReviewService = venueReviewService;
            _eventSender = eventSender;
            _mapper = mapper;
        }

        public async Task<AddVenueReviewResponse> Handle(AddVenueReviewCommand request, CancellationToken cancellationToken)
        {
            var addedVenueReview = await _venueReviewService.AddVenueReviewAsync(request);

            await _eventSender.SendEventAsync(EventBusTopics.VenueReview,
                addedVenueReview.FirstStoredEvent,
                cancellationToken
            );

            return new AddVenueReviewResponse {AddedVenueReview = _mapper.Map<VenueReviewDto>(addedVenueReview)};
        }
    }
}