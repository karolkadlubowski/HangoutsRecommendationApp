using Library.Database;
using Library.Shared.Models.Venue.Enums;

namespace Venue.API.Application.Database.PersistenceModels
{
    public class VenuePersistenceModel : BasePersistenceModel
    {
        public long VenueId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long LocationId { get; set; }
        public string CategoryId { get; set; }
        public long? CreatorId { get; set; }
        public VenueStatus Status { get; set; } = VenueStatus.Created;

        public virtual LocationPersistenceModel Location { get; set; }
    }
}