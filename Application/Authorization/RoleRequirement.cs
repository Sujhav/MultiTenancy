using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authorization
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public string RoleName { get; }

        public RoleRequirement(string role)
        {
            RoleName = role;
        }
    }
}
