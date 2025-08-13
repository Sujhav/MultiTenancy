using Domain.Common.Models;
using Domain.Users.ValuesObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users.Entities
{
    public sealed class RefreshToken : Entity<RefreshTokenId>
    {
        public string? Token { get; private set; }
        public AggregateRootId<Guid> UserId { get; private set; }
        public DateTime ExpiresOnUtc { get; private set; }
        private RefreshToken(RefreshTokenId Id, string? token, UserId userId, DateTime expiresOnUtc) : base(Id)
        {
            Token = token;
            UserId = userId;
            ExpiresOnUtc = expiresOnUtc;
        }

        public static RefreshToken CreateRefreshToken(string Token, UserId UserId, DateTime ExpiresOn)
        {
            return new RefreshToken(RefreshTokenId.CreateUnique(), Token, UserId, ExpiresOn);
        }

        public void UpdateToken(string newToken, DateTime newExpiry)
        {
            Token = newToken;
            ExpiresOnUtc = newExpiry;
        }


        private RefreshToken()
        {

        }


    }
}
