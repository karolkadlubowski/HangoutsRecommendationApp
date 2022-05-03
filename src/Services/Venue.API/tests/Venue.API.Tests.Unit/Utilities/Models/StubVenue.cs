using System.Collections.Generic;
using System.Linq;
using Venue.API.Domain.Entities.Models;

namespace Venue.API.Tests.Unit.Utilities.Models
{
    public class StubVenue : API.Domain.Entities.Venue
    {
        public StubVenue(long venueId, IEnumerable<Photo> photos)
        {
            VenueId = venueId;
            Photos = photos.ToList();
        }
    }
}