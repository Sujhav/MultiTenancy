using Application.Common.Dtos;
using Application.Common.Interfaces.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Users;
using Domain.Users.ValuesObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
namespace Infrastructure.Persistance.Repository
{
    internal class UserRepository : IUserRepository
    {
        private readonly MultiTenencyDbContext _context;
        public UserRepository(MultiTenencyDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task Add(Users users)
        {
            await _context.AddAsync(users);
            await _context.SaveChangesAsync();
        }

        public async Task<Users?> GetUsersByEmail(Email Email)
        {

            try
            {
                var data = await _context.Users
                             .Where(s => s.Email.Value == Email.Value).SingleOrDefaultAsync();
                if (data is null)
                {
                    return null;
                }
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;



        }


    }
}
