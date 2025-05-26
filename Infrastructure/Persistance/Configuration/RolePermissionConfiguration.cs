using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Configuration
{
    internal class RolePermissionConfiguration : IEntityTypeConfiguration<RolesPermissions>
    {
        public void Configure(EntityTypeBuilder<RolesPermissions> builder)
        {
            builder.HasKey(x => new { x.RolesId, x.PermissionId });
            builder.HasData(
                Create(Roles.RegisteredUser, Permissions.ReadMember),
                Create(Roles.RegisteredUser, Permissions.UpdateMembers)
                );
        }

        private static RolesPermissions Create(Roles roles, Permissions permission)
        {
            return new RolesPermissions
            {
                RolesId = roles.Id,
                PermissionId = (int)permission
            };
        }
    }
}
