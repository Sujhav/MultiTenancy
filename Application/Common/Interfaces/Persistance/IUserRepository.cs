using Application.Common.Dtos;
using Domain.Users;
using Domain.Users.ValuesObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Persistance
{
    public interface IUserRepository
    {
        Task<Users?> GetUsersByEmail(Email Email);
        Task Add(Users users);
    }
}
