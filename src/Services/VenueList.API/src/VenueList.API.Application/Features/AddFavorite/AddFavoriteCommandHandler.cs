using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Shared.Events.Abstractions;
using Library.Shared.Models.VenueList.Dtos;
using MediatR;
using VenueList.API.Application.Abstractions;

namespace VenueList.API.Application.Features.AddFavorite
{
    public class AddFavoriteCommandHandler : IRequestHandler<AddFavoriteCommand, AddFavoriteResponse>
    {
        private readonly IFavoriteService _favoriteService;
        //private readonly IEventSender _eventSender;
        private readonly IMapper _mapper;

        public AddFavoriteCommandHandler(IFavoriteService favoriteService,
            //IEventSender eventSender,
            IMapper mapper)
        {
            _favoriteService = favoriteService;
            //_eventSender = eventSender;
            _mapper = mapper;
        }

        public async Task<AddFavoriteResponse> Handle(AddFavoriteCommand request, CancellationToken cancellationToken)
        {
            var addedFavorite = await _favoriteService.AddVenueAsync(request);

            return new AddFavoriteResponse {AddedVenue = _mapper.Map<FavoriteDto>(addedFavorite)};
        }
    }
}