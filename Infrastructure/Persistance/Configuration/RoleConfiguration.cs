using Domain.Entities;
using Domain.Users;
using Domain.Users.ValuesObjects;
using Infrastructure.Persistance.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Configuration
{
    internal sealed class RoleConfiguration : IEntityTypeConfiguration<Roles>
    {
        public void Configure(EntityTypeBuilder<Roles> builder)
        {
            builder.ToTable(TableNames.Roles);

            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Permissions)
                .WithMany()
                .UsingEntity<RolesPermissions>();

            builder.HasMany(r => r.Users)
        .WithMany(u => u.Roles)
        .UsingEntity<RolesUsers>(
             // 1) How RolesUsers links back to the Users side:
             join => join
                 .HasOne(j => j.Users)
                 .WithMany(u => u.RolesUsers)   // nav on Users
                 .HasForeignKey(j => j.UserId),

             // 2) How RolesUsers links back to the Roles side:
             join => join
                 .HasOne(j => j.Role)
                 .WithMany(r => r.RolesUsers)   // nav on Roles
                 .HasForeignKey(j => j.RoleId),

             // 3) Everything else on the join table:
             join =>
             {
                 join.ToTable("RoleUsers");      // join‐table name
                 join.HasKey(j => new { j.UserId, j.RoleId });

                 // your DDD-value-object conversion:
                

                 // if RoleId were also a value‐object:
                 // join.Property(j => j.RoleId)
                 //     .HasConversion(…);
             }
        );


            builder.HasData(Roles.GetValues());
        }
    }
}
