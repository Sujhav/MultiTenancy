using Application.Common.Interfaces.Persistance;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Repository
{

    internal class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly MultiTenencyDbContext _context;
        private readonly IUserRepository _userRepository;
        public RefreshTokenRepository(MultiTenencyDbContext dbContext, IUserRepository userRepository)
        {
            _context = dbContext;
            _userRepository = userRepository;
        }
        public async Task Add(Users users, string RefreshToken)
        {
            var user = await _userRepository.GetUsersByEmail(users.Email);
            if (user is not null)
            {
                user.AddRefreshToken(token: RefreshToken, expiresOnUtc: DateTime.Now.AddDays(7));
                await _context.SaveChangesAsync();
            }
        }

    }
}
