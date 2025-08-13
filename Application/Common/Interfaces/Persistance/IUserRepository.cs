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
        Task<Domain.Users.Users?> GetUsersByEmail(Email Email);
        Task Add(Domain.Users.Users? users);
        Task<List<UsersDto>> GetAllUsers();
    }
}
