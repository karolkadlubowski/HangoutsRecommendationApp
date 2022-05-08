﻿using System.Collections.Generic;
using Library.Shared.Models.FileStorage.Dtos;
using Library.Shared.Models.Venue.Enums;

namespace Library.Shared.Models.Venue.Dtos
{
    public record VenueDto
    {
        public long VenueId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string CategoryId { get; init; }
        public long? CreatorId { get; init; }
        public VenueStatus Status { get; init; }

        public LocationDto Location { get; init; }

        public ICollection<FileDto> Photos { get; init; } = new HashSet<FileDto>();
    }
}