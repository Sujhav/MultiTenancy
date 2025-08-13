using Application.Authentication.Query.Login;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Query.RefreshToken
{
    public record RefreshTokenQuery(string token) : IRequest<LoginResponse>;
}
