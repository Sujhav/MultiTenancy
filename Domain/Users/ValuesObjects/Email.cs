using Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users.ValuesObjects
{
    public sealed class Email : ValueObject
    {
        public string Value { get; }
        private Email()
        {

        }

        private Email(string Email)
        {
            Value = Email;
        }
        public static Email GetEmail(string Email)
        {
            if (string.IsNullOrWhiteSpace(Email) || !Email.Contains("@"))
                throw new ArgumentException("Invalid Email Address");
            return new Email(Email);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
