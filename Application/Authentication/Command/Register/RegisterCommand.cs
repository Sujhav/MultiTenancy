using Application.Common.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Command.Register
{
    public record RegisterCommand(UsersDto Users) : IRequest<AuthenticationResult>;

}

