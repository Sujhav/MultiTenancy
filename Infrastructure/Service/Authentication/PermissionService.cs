using Domain.Entities;
using Domain.Users;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service.Authentication
{
    public class PermissionService : IPermissionService
    {
        private readonly MultiTenencyDbContext _context;

        public PermissionService(MultiTenencyDbContext context)
        {
            _context = context;
        }

        public async Task<HashSet<string>> GetPermissionAsync(Guid UserId)
        {
            ICollection<Roles>[] role = await _context.Set<Users>().Where(x => x.Id.Value == UserId)
                                     .Include(x => x.Roles)
                                     .ThenInclude(x => x.Permissions)
                                     .Select(x => x.Roles)
                                     .ToArrayAsync();
            return role
                .SelectMany(s => s)
                .SelectMany(s => s.Permissions)
                .Select(x => x.Name)
                .ToHashSet();
        }
    }
}
