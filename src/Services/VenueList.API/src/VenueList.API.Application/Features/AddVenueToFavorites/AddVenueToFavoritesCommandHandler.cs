using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Shared.Exceptions;
using Library.Shared.HttpAccessor;
using Library.Shared.Logging;
using Library.Shared.Models.VenueList.Dtos;
using MediatR;
using VenueList.API.Application.Abstractions;

namespace VenueList.API.Application.Features.AddVenueToFavorites
{
    public class AddVenueToFavoritesCommandHandler : IRequestHandler<AddVenueToFavoritesCommand, AddVenueToFavoritesResponse>
    {
        private readonly IFavoriteService _favoriteService;
        private readonly ICategoriesCacheRepository _cacheRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IReadOnlyHttpAccessor _httpAccessor;


        public AddVenueToFavoritesCommandHandler(IFavoriteService favoriteService,
            ICategoriesCacheRepository cacheRepository,
            IMapper mapper, ILogger logger,
            IReadOnlyHttpAccessor httpAccessor
        )
        {
            _favoriteService = favoriteService;
            _cacheRepository = cacheRepository;
            _mapper = mapper;
            _logger = logger;
            _httpAccessor = httpAccessor;
        }

        public async Task<AddVenueToFavoritesResponse> Handle(AddVenueToFavoritesCommand request, CancellationToken cancellationToken)
        {
            var category = await _cacheRepository.FindCategoryByNameAsync(request.CategoryName)
                           ?? throw new EntityNotFoundException($"Category with name '{request.CategoryName}' does not exist");
            _logger.Trace($"Category #{category.CategoryId} with name '{category.Name}' found in the memory cache");

            var addedFavoriteVenue = await _favoriteService.AddVenueToFavoritesAsync(request, _httpAccessor.CurrentUserId);

            return new AddVenueToFavoritesResponse {AddedVenue = _mapper.Map<FavoriteDto>(addedFavoriteVenue)};
        }
    }
}