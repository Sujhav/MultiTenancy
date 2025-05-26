using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Models
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        public abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj is null || obj.GetType() != GetType()) return false;

            ValueObject objValue = (ValueObject)obj;

            return GetEqualityComponents().SequenceEqual(objValue.GetEqualityComponents());
        }

        public static bool operator ==(ValueObject left, ValueObject right)
        {
            return Equals(left, right);
        }
        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return !Equals(left, right);
        }
        public bool Equals(ValueObject? obj)
        {
            return Equals((object?)obj);
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(s => s?.GetHashCode() ?? 0)
                .Aggregate((x, y) => x ^ y);
        }



    }
    public class Price : ValueObject
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }

        public Price(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }
    }
}
