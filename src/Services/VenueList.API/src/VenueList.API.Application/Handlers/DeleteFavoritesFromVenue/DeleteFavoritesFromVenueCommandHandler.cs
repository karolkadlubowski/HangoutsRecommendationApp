using System.Threading;
using System.Threading.Tasks;
using Library.Shared.Logging;
using MediatR;
using VenueList.API.Application.Database.Repositories;

namespace VenueList.API.Application.Handlers.DeleteFavoritesFromVenue
{
    public class DeleteFavoritesFromVenueCommandHandler : IRequestHandler<DeleteFavoritesFromVenueCommand, DeleteFavoritesFromVenueResponse>
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly ILogger _logger;

        public DeleteFavoritesFromVenueCommandHandler(IFavoriteRepository favoriteRepository, ILogger logger)
        {
            _favoriteRepository = favoriteRepository;
            _logger = logger;
        }

        public async Task<DeleteFavoritesFromVenueResponse> Handle(DeleteFavoritesFromVenueCommand request, CancellationToken cancellationToken)
        {
            await _favoriteRepository.DeleteFavoriteByVenueIdAsync(request.VenueId);

            _logger.Info($"All favorites were successfully deleted for venue #{request.VenueId}");

            return new DeleteFavoritesFromVenueResponse {VenueId = request.VenueId};
        }
    }
}