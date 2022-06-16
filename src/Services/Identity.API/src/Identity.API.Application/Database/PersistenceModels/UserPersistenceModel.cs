using Library.Database;

namespace Identity.API.Application.Database.PersistenceModels
{
    public class UserPersistenceModel : BasePersistenceModel
    {
        public long UserId { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}