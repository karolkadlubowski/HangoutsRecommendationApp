using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Shared.Models.VenueList.Dtos;
using MediatR;
using VenueList.API.Application.Abstractions;

namespace VenueList.API.Application.Features.AddVenue
{
    public class AddVenueCommandHandler : IRequestHandler<AddVenueCommand, AddVenueResponse>
    {
        private readonly IVenueService _venueService;
        //private readonly IEventSender _eventSender;
        private readonly IMapper _mapper;

        public AddVenueCommandHandler(IVenueService venueReviewService,
            //IEventSender eventSender,
            IMapper mapper)
        {
            _venueService = venueReviewService;
            //_eventSender = eventSender;
            _mapper = mapper;
        }

        public async Task<AddVenueResponse> Handle(AddVenueCommand request, CancellationToken cancellationToken)
        {
            var addedVenueReview = await _venueService.AddVenueAsync(request);

            return new AddVenueResponse {AddedVenue = _mapper.Map<VenueDto>(addedVenueReview)};
        }
    }
}