using Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users.ValuesObjects
{
    public sealed class RefreshTokenId : ValueObject
    {
        public Guid Value { get; }

        private RefreshTokenId(Guid value)
        {
            Value = value;
        }

        public static RefreshTokenId CreateUnique()
        {
            return new RefreshTokenId(Guid.NewGuid());

        }

        public static RefreshTokenId CreateNew(Guid value)
        {
            return new RefreshTokenId(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        private RefreshTokenId() { }
    }
}
