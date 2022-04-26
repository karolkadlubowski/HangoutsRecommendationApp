using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Shared.Models.Venue.Dtos;
using MediatR;
using Venue.API.Application.Abstractions;

namespace Venue.API.Application.Features.GetVenue
{
    public class GetVenueQueryHandler : IRequestHandler<GetVenueQuery, GetVenueResponse>
    {
        private readonly IReadOnlyVenueService _venueService;
        private readonly IMapper _mapper;

        public GetVenueQueryHandler(IReadOnlyVenueService venueService,
            IMapper mapper)
        {
            _venueService = venueService;
            _mapper = mapper;
        }

        public async Task<GetVenueResponse> Handle(GetVenueQuery request, CancellationToken cancellationToken)
            => new GetVenueResponse
            {
                Venue = _mapper.Map<VenueDto>(await _venueService.GetVenueWithPhotosAsync(request))
            };
    }
}