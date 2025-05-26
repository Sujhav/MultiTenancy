using Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users.ValuesObjects
{
    public sealed class UserId : AggregateRootId<Guid>
    {
        public override Guid Value { get; protected set; }

        private UserId(Guid value)
        {
            Value = value;
        }

        public static UserId CreateUnique()
        {
            var newid = Guid.NewGuid();
            var data = new UserId(newid);
            return data;
        }

        public static UserId Create(Guid value)
        {
            return new UserId(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            //var components=new List<object>();
            //components.Add(Value);
            //return components;
            yield return Value;
        }

        private UserId()
        {

        }
    }
}
