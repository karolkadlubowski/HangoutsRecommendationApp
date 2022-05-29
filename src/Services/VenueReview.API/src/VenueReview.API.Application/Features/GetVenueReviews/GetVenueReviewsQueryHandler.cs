using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Shared.Models.VenueReview.Dtos;
using MediatR;
using VenueReview.API.Application.Abstractions;

namespace VenueReview.API.Application.Features
{
    public class GetVenueReviewsQueryHandler : IRequestHandler<GetVenueReviewsQuery,GetVenueReviewsResponse>
    {
        private readonly IReadOnlyVenueReviewService _venueReviewService;
        private readonly IMapper _mapper;

        public GetVenueReviewsQueryHandler(IReadOnlyVenueReviewService venueReviewService, IMapper mapper)
        {
            _venueReviewService = venueReviewService;
            _mapper = mapper;
        }

        public async Task<GetVenueReviewsResponse> Handle(GetVenueReviewsQuery request, CancellationToken cancellationToken)
            => new GetVenueReviewsResponse {VenueReviews = _mapper.Map<IReadOnlyList<VenueReviewDto>>(await _venueReviewService.GetVenueReviewsAsync(request))};
    }
}