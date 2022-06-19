using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Shared.Exceptions;
using Library.Shared.Logging;
using Library.Shared.Models.VenueList.Dtos;
using MediatR;
using VenueList.API.Application.Abstractions;

namespace VenueList.API.Application.Features.AddFavorite
{
    public class AddFavoriteCommandHandler : IRequestHandler<AddFavoriteCommand, AddFavoriteResponse>
    {
        private readonly IFavoriteService _favoriteService;
        private readonly ICategoriesCacheRepository _cacheRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;


        public AddFavoriteCommandHandler(IFavoriteService favoriteService, ICategoriesCacheRepository cacheRepository,
            IMapper mapper, ILogger logger)
        {
            _favoriteService = favoriteService;
            _cacheRepository = cacheRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AddFavoriteResponse> Handle(AddFavoriteCommand request, CancellationToken cancellationToken)
        {
            var category = await _cacheRepository.FindCategoryByNameAsync(request.CategoryName)
                           ?? throw new EntityNotFoundException($"Category with name '{request.CategoryName}' does not exist");
            _logger.Trace($"Category #{category.CategoryId} with name '{category.Name}' found in the memory cache");

            var addedFavorite = await _favoriteService.AddVenueAsync(request);

            return new AddFavoriteResponse {AddedVenue = _mapper.Map<FavoriteDto>(addedFavorite)};
        }
    }
}