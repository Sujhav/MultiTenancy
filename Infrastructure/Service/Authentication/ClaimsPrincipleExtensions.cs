using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service.Authentication
{
    public static class ClaimsPrincipleExtensions
    {
        public static Guid? GetUserId(this ClaimsPrincipal user)
        {
            var id = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            return id != null ? Guid.Parse(id) : null;
        }
    }
}
