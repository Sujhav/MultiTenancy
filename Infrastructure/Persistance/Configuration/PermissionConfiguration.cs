using Domain.Entities;
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
    internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable(TableNames.Permissions);

            builder.HasKey(p => p.Id);

            IEnumerable<Permission> permissions = Enum.GetValues<Domain.Enums.Permissions>().Select(p => new Permission
            {
                Id = (int)p,
                Name = p.ToString()

            });

            builder.HasData(permissions);
        }
    }
}
