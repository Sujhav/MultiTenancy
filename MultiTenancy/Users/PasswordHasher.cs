using Application.Common.Interfaces.Authentication;
using System.Security.Cryptography;
namespace MultiTenancy.Users
{
    public sealed class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iteration = 100000;

        private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;

        public string HashPassoword(string Password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(Password, salt, Iteration, Algorithm, HashSize);

            return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";

        }
        public bool VerifyPassword(string Password, string PasswordHash)
        {
            string[] parts = PasswordHash.Split("-");
            byte[] hash = Convert.FromHexString(parts[0]);
            byte[] salt = Convert.FromHexString(parts[1]);

            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(Password, salt, Iteration, Algorithm, HashSize);

            return CryptographicOperations.FixedTimeEquals(hash, inputHash);

        }

     
    }
}
