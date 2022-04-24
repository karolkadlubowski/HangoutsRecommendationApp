using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Shared.Models.Pagination;
using Library.Shared.Models.Venue.Dtos;
using MediatR;
using Venue.API.Application.Abstractions;

namespace Venue.API.Application.Features.GetVenues
{
    public class GetVenuesQueryHandler : IRequestHandler<GetVenuesQuery, GetVenuesResponse>
    {
        private readonly IReadOnlyVenueService _venueService;
        private readonly IMapper _mapper;

        public GetVenuesQueryHandler(IReadOnlyVenueService venueService,
            IMapper mapper)
        {
            _venueService = venueService;
            _mapper = mapper;
        }

        public async Task<GetVenuesResponse> Handle(GetVenuesQuery request, CancellationToken cancellationToken)
        {
            var venues = await _venueService.GetVenuesAsync(request);
            var venuesToReturn = _mapper.Map<IPagedList<VenueDto>>(venues);

            return new GetVenuesResponse
            {
                Venues = venuesToReturn,
                Pagination = PaginationResponseDecorator<VenueDto>.Create(venuesToReturn)
            };
        }
    }
}