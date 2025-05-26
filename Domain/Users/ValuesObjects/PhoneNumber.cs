using Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users.ValuesObjects
{
    public sealed class PhoneNumber : ValueObject
    {
        public string Value { get; private set; }

        private PhoneNumber(string value)
        {
            Value = value;
        }

        public static PhoneNumber GetPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Phone number cannot be empty or null.");

            if (!phoneNumber.All(char.IsDigit))
                throw new ArgumentException("Phone number must contain only digits.");


            return new PhoneNumber(phoneNumber);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
#pragma warning disable
        private PhoneNumber()
        {

        }
#pragma warning restore
    }
}
