using Application.Common.Dtos;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication
{

    public record AuthenticationResult(Users users, string Token);

}
