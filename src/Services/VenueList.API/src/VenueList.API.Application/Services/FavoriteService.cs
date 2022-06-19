using System;
using System.Threading.Tasks;
using AutoMapper;
using Library.Shared.Exceptions;
using Library.Shared.Logging;
using VenueList.API.Application.Abstractions;
using VenueList.API.Application.Database.PersistenceModels;
using VenueList.API.Application.Database.Repositories;
using VenueList.API.Application.Features.AddFavorite;
using VenueList.API.Application.Features.DeleteFavorite;
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
            var favorite = Favorite.Create(command.VenueId, command.UserId, command.Name, command.Description, command.CategoryName, command.CreatorId);
            
            if(await _favoriteRepository.AnyFavoriteExistsAsync(command.VenueId,command.UserId))
                throw new DuplicateExistsException($"User #{favorite.CreatorId} has already added venue #{favorite.VenueId} to the database");


            var favoritePersistenceModel = await _favoriteRepository.InsertFavoriteAsync(favorite)
                                        ?? throw new DatabaseOperationException($"Inserting venue with id #{favorite.VenueId} for a user with id #{favorite.UserId} to the database failed");

            favorite = _mapper.Map<FavoritePersistenceModel, Domain.Entities.Favorite>(favoritePersistenceModel);

            _logger.Info($"Venue with id #{favorite.VenueId} for a user with id #{favorite.UserId} added to the database successfully");

            return favorite;
        }

        public async Task<string> DeleteFavoriteAsync(DeleteFavoriteCommand command)
        {
            try
            {
                if (!await _favoriteRepository.DeleteFavoriteAsync(command.FavoriteId))
                    throw new EntityNotFoundException($"Favorite #{command.FavoriteId} was not deleted from the database because it was not found");

                _logger.Info($"Favorite #{command.FavoriteId} deleted from the database successfully");

                return command.FavoriteId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}