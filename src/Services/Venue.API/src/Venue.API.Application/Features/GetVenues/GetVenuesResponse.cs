﻿using System.Collections.Generic;
using System.Collections.Immutable;
using Library.Shared.Models.Pagination;
using Library.Shared.Models.Response;
using Library.Shared.Models.Venue.Dtos;

namespace Venue.API.Application.Features.GetVenues
{
    public record GetVenuesResponse : BaseResponse
    {
        public IReadOnlyList<VenueListDto> Venues { get; init; } = ImmutableList<VenueListDto>.Empty;
        public PaginationResponseDecorator Pagination { get; init; }

        public GetVenuesResponse(Error error = null) : base(error)
        {
        }
    }
}