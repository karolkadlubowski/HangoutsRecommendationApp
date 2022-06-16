using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Identity.API.Application.Abstractions;
using Konscious.Security.Cryptography;

namespace Identity.API.Application.Services
{
    public class Argo2PasswordHashService : IPasswordHashService
    {
        private const int DegreeOfParallelism = 1;
        private const int Iterations = 2;
        private const int MemorySizeInMb = 15 * 1024;

        public byte[] CreatePasswordSalt()
        {
            var saltBinary = new byte[16];
            var rngCryptoService = new RNGCryptoServiceProvider();
            rngCryptoService.GetBytes(saltBinary);

            return saltBinary;
        }

        public byte[] HashPassword(string password, byte[] salt)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));

            argon2.Salt = salt;
            argon2.DegreeOfParallelism = DegreeOfParallelism;
            argon2.Iterations = Iterations;
            argon2.MemorySize = MemorySizeInMb;

            return argon2.GetBytes(16);
        }

        public bool VerifyPasswordHash(string password, byte[] salt, byte[] hash)
            => hash.SequenceEqual(HashPassword(password, salt));
    }
}