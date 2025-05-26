using Domain.Enums;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Roles : Enumeration<Roles>
    {
        //public static readonly Roles RegisteredUser = new Registered();
        public static readonly Roles RegisteredUser = new Roles(1, "Registered");

        public Roles(int id, string name) : base(id, name)
        {
        }

        private sealed class Registered : Roles
        {
            public Registered() : base(1, "Registered")
            {

            }
        }

        public ICollection<Permission> Permissions { get; set; }
        public ICollection<Domain.Users.Users> Users { get; set; }
        public ICollection<RolesUsers> RolesUsers { get; set; }



    }

}
