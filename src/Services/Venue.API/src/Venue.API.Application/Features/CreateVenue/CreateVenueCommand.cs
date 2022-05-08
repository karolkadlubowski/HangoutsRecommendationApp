﻿using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Venue.API.Application.Features.CreateVenue
{
    public record CreateVenueCommand : IRequest<CreateVenueResponse>
    {
        public string VenueName { get; init; }
        public string Description { get; init; }
        public string CategoryName { get; init; }

        public string Address { get; init; }

        public double Latitude { get; init; }
        public double Longitude { get; init; }

        public ICollection<IFormFile> Photos { get; init; } = new List<IFormFile>();
    }
}