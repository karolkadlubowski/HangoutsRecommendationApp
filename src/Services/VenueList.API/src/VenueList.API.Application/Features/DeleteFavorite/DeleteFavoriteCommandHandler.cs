using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VenueList.API.Application.Abstractions;

namespace VenueList.API.Application.Features.DeleteFavorite
{
    public class DeleteFavoriteCommandHandler : IRequestHandler<DeleteFavoriteCommand, DeleteFavoriteResponse>
    {
        private readonly IFavoriteService _favoriteService;

        public DeleteFavoriteCommandHandler(IFavoriteService venueReviewService)
        {
            _favoriteService = venueReviewService;
        }
        
        public async Task<DeleteFavoriteResponse> Handle(DeleteFavoriteCommand request, CancellationToken cancellationToken)
        {
            var deletedFavoriteId = await _favoriteService.DeleteFavoriteAsync(request);
            return new DeleteFavoriteResponse {DeletedFavoriteId = deletedFavoriteId};
        }
    }
}