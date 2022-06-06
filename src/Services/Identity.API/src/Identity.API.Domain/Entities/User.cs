using Identity.API.Domain.ValueObjects;
using Library.Shared.Models;

namespace Identity.API.Domain.Entities
{
    public class User : RootEntity
    {
        public long UserId { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public User(string email, byte[] passwordSalt, byte[] passwordHash)
        {
            Email = new UserEmail(email);
            PasswordSalt = passwordSalt;
            PasswordHash = passwordHash;
        }
    }
}