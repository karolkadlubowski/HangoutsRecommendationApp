using System.Collections.Generic;
using System.Linq;
using Library.Shared.Exceptions;
using Library.Shared.Models;
using Library.Shared.Models.Venue.Enums;
using Venue.API.Domain.Entities.Models;
using Venue.API.Domain.Validation;
using Venue.API.Domain.ValueObjects;

namespace Venue.API.Domain.Entities
{
    public class Venue : RootEntity
    {
        public long VenueId { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public long? LocationId { get; protected set; }
        public string CategoryId { get; protected set; }
        public long? CreatorId { get; protected set; }
        public VenueStatus Status { get; protected set; } = VenueStatus.Created;
        public VenuePersistState PersistState { get; protected set; } = VenuePersistState.NotPersisted;

        public ICollection<Photo> Photos { get; protected set; } = new HashSet<Photo>();

        public static Venue CreateDefault(string name, long locationId, string categoryId)
            => new Venue
            {
                Name = new VenueName(name),
                LocationId = locationId,
                CategoryId = new CategoryId(categoryId)
            };

        public static Venue CreateWithoutLocation(string name, string categoryId)
            => new Venue { Name = new VenueName(name), CategoryId = new CategoryId(categoryId) };

        public void SetDescription(string description)
            => Description = new VenueDescription(description);

        public void AssignLocation(long locationId)
            => LocationId = locationId;

        public void CreatedBy(long creatorId)
            => CreatorId = creatorId;

        public void UpdatePersistState(VenuePersistState persistState)
            => PersistState = persistState;

        public void Accept()
            => Status = VenueStatus.Accepted;

        public void AddPhotos(IEnumerable<Photo> photos)
        {
            if (Photos.Count + photos.Count() > ValidationRules.MaxPhotosCount)
                throw new ValidationException($"Venu can contain only {ValidationRules.MaxPhotosCount} photos");

            foreach (var photo in photos)
                Photos.Add(photo);
        }
    }
}