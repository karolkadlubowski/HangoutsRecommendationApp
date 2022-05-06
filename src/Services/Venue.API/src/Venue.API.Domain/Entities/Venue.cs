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
        public long LocationId { get; protected set; }
        public string CategoryId { get; protected set; }
        public long? CreatorId { get; protected set; }
        public VenueStatus Status { get; protected set; } = VenueStatus.Created;

        public Location Location { get; protected set; }

        public ICollection<Photo> Photos { get; protected set; } = new HashSet<Photo>();

        public static Venue CreateDefault(string name, string categoryId)
            => new Venue
            {
                Name = new VenueName(name),
                CategoryId = new CategoryId(categoryId)
            };

        public void Update(string name, string categoryId, string description,
            string address, double latitude, double longitude, long userId)
        {
            if (CreatorId != userId)
                throw new InsufficientPermissionsException($"User #{userId} has not permissions to update venue #{VenueId}");

            Name = new VenueName(name);
            CategoryId = new CategoryId(categoryId);
            Description = new VenueDescription(description);
            Location.Update(address, latitude, longitude);
        }

        public void SetDescription(string description)
            => Description = new VenueDescription(description);

        public void AssignLocation(long locationId)
            => LocationId = locationId;

        public void CreatedBy(long creatorId)
            => CreatorId = creatorId;

        public void Accept()
            => Status = VenueStatus.Accepted;

        public void SetLocationWithCoordinates(string address, double latitude, double longitude)
        {
            Location = Location.Create(address);
            Location.SetCoordinates(latitude, longitude);
        }

        public void AddPhotos(IEnumerable<Photo> photos)
        {
            if (Photos.Count + photos.Count() > ValidationRules.MaxPhotosCount)
                throw new ValidationException($"Venu can contain only {ValidationRules.MaxPhotosCount} photos");

            foreach (var photo in photos)
                Photos.Add(photo);
        }
    }
}