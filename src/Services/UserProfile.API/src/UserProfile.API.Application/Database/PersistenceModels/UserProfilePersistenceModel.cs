using Library.Database;

namespace UserProfile.API.Application.Database.PersistenceModels
{
    public class UserProfilePersistenceModel : BasePersistenceModel
    {
        public long UserId { get; protected set; }
        public string EmailAddress { get; protected set; }
    }
}