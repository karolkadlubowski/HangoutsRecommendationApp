using System.Threading;
using System.Threading.Tasks;
using Library.Shared.HttpAccessor;
using MediatR;
using VenueList.API.Application.Abstractions;

namespace VenueList.API.Application.Features.DeleteFavorite
{
    public class DeleteFavoriteCommandHandler : IRequestHandler<DeleteFavoriteCommand, DeleteFavoriteResponse>
    {
        private readonly IFavoriteService _favoriteService;
        private readonly IReadOnlyHttpAccessor _httpAccessor;

        public DeleteFavoriteCommandHandler(IFavoriteService favoriteService, IReadOnlyHttpAccessor httpAccessor)
        {
            _favoriteService = favoriteService;
            _httpAccessor = httpAccessor;
        }

        public async Task<DeleteFavoriteResponse> Handle(DeleteFavoriteCommand request, CancellationToken cancellationToken)
        {
            var deletedFavoriteId = await _favoriteService.DeleteFavoriteAsync(request, _httpAccessor.CurrentUserId);
            return new DeleteFavoriteResponse {DeletedFavoriteId = deletedFavoriteId};
        }
    }
}