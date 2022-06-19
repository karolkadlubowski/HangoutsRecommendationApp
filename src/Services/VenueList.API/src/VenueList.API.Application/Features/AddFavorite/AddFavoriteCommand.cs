﻿using MediatR;

namespace VenueList.API.Application.Features.AddFavorite
{
    public record AddFavoriteCommand : IRequest<AddFavoriteResponse>
    {
        public long VenueId { get; init; }
        public long UserId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string CategoryName { get; init; }
        public long? CreatorId { get; init; }
    }
}