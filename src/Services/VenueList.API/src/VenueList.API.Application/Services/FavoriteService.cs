using System.Threading.Tasks;
using AutoMapper;
using Library.Shared.Exceptions;
using Library.Shared.Logging;
using VenueList.API.Application.Abstractions;
using VenueList.API.Application.Database.PersistenceModels;
using VenueList.API.Application.Database.Repositories;
using VenueList.API.Application.Features.AddFavorite;
using VenueList.API.Domain.Entities;

namespace VenueList.API.Application.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public FavoriteService(
            IFavoriteRepository favoriteRepository,
            IMapper mapper,
            ILogger logger)
        {
            _favoriteRepository = favoriteRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Favorite> AddVenueAsync(AddFavoriteCommand command)
        {
            var favorite = Favorite.Create(command.VenueId, command.UserId, command.Name, command.Description, command.CategoryId, command.CreatorId);

            var favoritePersistenceModel = await _favoriteRepository.InsertFavoriteAsync(favorite)
                                        ?? throw new DatabaseOperationException($"Inserting venue with id #{favorite.VenueId} for a user with id #{favorite.UserId} to the database failed");

            favorite = _mapper.Map<FavoritePersistenceModel, Domain.Entities.Favorite>(favoritePersistenceModel);

            _logger.Info($"Venue with id #{favorite.VenueId} for a user with id #{favorite.UserId} added to the database successfully");

            return favorite;
        }
    }
}