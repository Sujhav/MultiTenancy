using Domain.Common.Models;
using Domain.Users;
using Domain.Users.ValuesObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RolesUsers
    {
        public AggregateRootId<Guid> UserId { get; set; }

        public Domain.Users.Users Users { get; set; }
        public int RoleId { get; set; }
        public Roles Role { get; set; }
    }
}
