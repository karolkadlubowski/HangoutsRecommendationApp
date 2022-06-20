using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Library.Shared.Exceptions;
using Library.Shared.Logging;
using Library.Shared.Models.Pagination;
using Library.Shared.Models.Pagination.Models;
using VenueList.API.Application.Abstractions;
using VenueList.API.Application.Database;
using VenueList.API.Application.Database.PersistenceModels;
using VenueList.API.Application.Database.Repositories;
using VenueList.API.Application.Features.AddVenueToFavorites;
using VenueList.API.Application.Features.DeleteFavorite;
using VenueList.API.Application.Features.GetUserFavorites;
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

        public async Task<Favorite> AddVenueToFavoritesAsync(AddVenueToFavoritesCommand command, long userId)
        {
            var favorite = Favorite.Create(command.VenueId, userId, command.Name, command.Description, command.CategoryName, command.CreatorId);

            if (await _favoriteRepository.AnyFavoriteExistsAsync(command.VenueId, userId))
                throw new DuplicateExistsException($"User #{favorite.UserId} has already added venue #{favorite.VenueId} to the database");


            var favoritePersistenceModel = await _favoriteRepository.InsertFavoriteAsync(favorite)
                                           ?? throw new DatabaseOperationException($"Inserting venue #{favorite.VenueId} for a user with id #{favorite.UserId} to the database failed");

            favorite = _mapper.Map<FavoritePersistenceModel, Domain.Entities.Favorite>(favoritePersistenceModel);

            _logger.Info($"Venue #{favorite.VenueId} for a user with id #{favorite.UserId} added to the database successfully");

            return favorite;
        }

        public async Task<long> DeleteFavoriteAsync(DeleteFavoriteCommand command, long userId)
        {
            try
            {
                if (!await _favoriteRepository.DeleteFavoriteByVenueIdAndUserIdAsync(command.VenueId, userId))
                    throw new EntityNotFoundException($"Venue #{command.VenueId} was not deleted from the database because it was not found");

                _logger.Info($"Venue #{command.VenueId} deleted from the database successfully");

                return command.VenueId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<PaginationTuple<Favorite>> GetUserFavoritesAsync(GetUserFavoritesQuery query, long userId)
        {
            var favoritesPersistenceModels = await _favoriteRepository.GetPaginatedFavoritesAsync(query, userId);
            var pagination = PaginationResponseDecorator.Create(favoritesPersistenceModels);

            _logger.Info($"Favorites: {favoritesPersistenceModels.CurrentPage} page with {favoritesPersistenceModels.CurrentCount} records loaded from the database");

            return new PaginationTuple<Domain.Entities.Favorite>(_mapper.Map<IReadOnlyList<Domain.Entities.Favorite>>(favoritesPersistenceModels), pagination);
        }
    }
}