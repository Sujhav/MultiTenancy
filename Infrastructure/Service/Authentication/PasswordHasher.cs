using Application.Common.Interfaces.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service.Authentication
{
    public sealed class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16;
        public const int HashSize = 32;
        public const int Iteration = 100000;

        private static readonly HashAlgorithmName algorithmName = HashAlgorithmName.SHA512;

        public string HashPassoword(string Password)
        {
            var Salt = RandomNumberGenerator.GetBytes(SaltSize);
            var Hash = Rfc2898DeriveBytes.Pbkdf2(Password, Salt, Iteration, algorithmName, HashSize);

            var passwordHash = $"{Convert.ToHexString(Hash)}-{Convert.ToHexString(Salt)}";
            return passwordHash;
        }

        public bool VerifyPassword(string Password, string PasswordHash)
        {
            string[] parts = PasswordHash.Split('-');
            var hash = Convert.FromHexString(parts[0]);
            var salt = Convert.FromHexString(parts[1]);

            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(Password, salt, Iteration, algorithmName, HashSize);

            return CryptographicOperations.FixedTimeEquals(hash, inputHash);

        }
    }
}
