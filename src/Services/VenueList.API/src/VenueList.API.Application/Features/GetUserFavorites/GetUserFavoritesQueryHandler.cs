using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Shared.HttpAccessor;
using Library.Shared.Models.Pagination;
using Library.Shared.Models.VenueList.Dtos;
using MediatR;
using VenueList.API.Application.Abstractions;

namespace VenueList.API.Application.Features.GetUserFavorites
{
    public class GetUserFavoritesQueryHandler : IRequestHandler<GetUserFavoritesQuery, GetUserFavoritesResponse>
    {
        private readonly IReadOnlyFavoriteService _favoriteService;
        private readonly IMapper _mapper;
        private readonly IReadOnlyHttpAccessor _httpAccessor;

        public GetUserFavoritesQueryHandler(IReadOnlyFavoriteService favoriteService,
            IMapper mapper, IReadOnlyHttpAccessor httpAccessor)
        {
            _favoriteService = favoriteService;
            _mapper = mapper;
            _httpAccessor = httpAccessor;
        }

        public async Task<GetUserFavoritesResponse> Handle(GetUserFavoritesQuery request, CancellationToken cancellationToken)
        {
            var favoritesPaginationTuple = await _favoriteService.GetUserFavoritesAsync(request, _httpAccessor.CurrentUserId);
            var favoritesToReturn = _mapper.Map<PagedList<FavoriteDto>>(favoritesPaginationTuple.List);

            return new GetUserFavoritesResponse
            {
                Favorites = favoritesToReturn,
                Pagination = favoritesPaginationTuple.Pagination
            };
        }
    }
}