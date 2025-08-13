using Application.Common.Dtos;
using Application.Common.Errors;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppUsers.Query
{
    public record GetAllUsersQuey() : IRequest<CustomResult<List<UsersDto>>>;


}
