using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Shared.Models.Pagination;
using Library.Shared.Models.Venue.Dtos;
using Library.Shared.Models.VenueList.Dtos;
using MediatR;
using VenueList.API.Application.Abstractions;

namespace VenueList.API.Application.Features.GetFavorites
{
    public class GetFavoritesQueryHandler : IRequestHandler<GetFavoritesQuery, GetFavoritesResponse>
    {
        private readonly IReadOnlyFavoriteService _favoriteService;
        private readonly IMapper _mapper;

        public GetFavoritesQueryHandler(IReadOnlyFavoriteService favoriteService,
            IMapper mapper)
        {
            _favoriteService = favoriteService;
            _mapper = mapper;
        }

        public async Task<GetFavoritesResponse> Handle(GetFavoritesQuery request, CancellationToken cancellationToken)
        {
            var favoritesPaginationTuple = await _favoriteService.GetFavoritesAsync(request);
            var favoritesToReturn = _mapper.Map<PagedList<FavoriteDto>>(favoritesPaginationTuple.List);

            return new GetFavoritesResponse
            {
                Favorites = favoritesToReturn,
                Pagination = favoritesPaginationTuple.Pagination
            };
        }
    }
}