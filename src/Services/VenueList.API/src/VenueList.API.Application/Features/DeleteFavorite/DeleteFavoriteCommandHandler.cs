using System.Threading;
using System.Threading.Tasks;
using Library.EventBus;
using Library.Shared.Events.Abstractions;
using Library.Shared.HttpAccessor;
using Library.Shared.Models.VenueList.Events.DataModels;
using Library.Shared.Models.VenueReview.Events;
using Library.Shared.Models.VenueReview.Events.DataModels;
using MediatR;
using VenueList.API.Application.Abstractions;
using VenueList.API.Domain.Entities;
using VenueList.API.Domain.ValueObjects;

namespace VenueList.API.Application.Features.DeleteFavorite
{
    public class DeleteFavoriteCommandHandler : IRequestHandler<DeleteFavoriteCommand, DeleteFavoriteResponse>
    {
        private readonly IFavoriteService _favoriteService;
        private readonly IReadOnlyHttpAccessor _httpAccessor;
        private readonly IEventSender _eventSender;

        public DeleteFavoriteCommandHandler(IFavoriteService favoriteService, IReadOnlyHttpAccessor httpAccessor,
            IEventSender eventSender)
        {
            _favoriteService = favoriteService;
            _httpAccessor = httpAccessor;
            _eventSender = eventSender;
        }

        public async Task<DeleteFavoriteResponse> Handle(DeleteFavoriteCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = _httpAccessor.CurrentUserId;
            var deletedFavoriteId = await _favoriteService.DeleteFavoriteAsync(request, currentUserId);

            await _eventSender.SendEventAsync(EventBusTopics.VenueReview,
                EventFactory<VenueReviewDeletedEvent>.CreateEvent(new FavoriteId(request.VenueId, currentUserId).Value,
                    new VenueDeletedFromFavoritesEventDataModel
                    {
                        VenueId = request.VenueId,
                        UserId = currentUserId
                    }),
                cancellationToken);

            return new DeleteFavoriteResponse {DeletedFavoriteId = deletedFavoriteId};
        }
    }
}