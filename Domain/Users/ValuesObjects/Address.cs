using Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users.ValuesObjects
{
    public sealed class Address : ValueObject
    {
        public string District { get; private set; }
        public string City { get; private set; }

#pragma warning disable
        private Address()
        {
        }
#pragma warning restore
        private Address(string district, string city)
        {
            District = district;
            City = city;
        }

        public static Address GetAddress(string district, string city)
        {
            if (string.IsNullOrWhiteSpace(district) && string.IsNullOrEmpty(city))
                throw new ArgumentException("Invalid  Address");
            return new Address(district, city);
        }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return District;
            yield return City;
        }
    }
}
