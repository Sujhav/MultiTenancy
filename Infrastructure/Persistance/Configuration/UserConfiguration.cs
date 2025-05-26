using Domain.Common.Models;
using Domain.Entities;
using Domain.Users;
using Domain.Users.ValuesObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            ConfigureUsers(builder);
        }

        public void ConfigureUsers(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable("AppUsers");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id)
                .ValueGeneratedNever()
                .HasConversion(usersid => usersid.Value,
                    value => UserId.Create(value));

            builder.Property(s => s.FirstName).IsRequired()
            .HasMaxLength(30);


            builder.OwnsOne(u => u.Email, em =>
            {
                em.Property(s => s.Value)
                .HasColumnName("Email")
                .IsRequired();
                em.HasIndex(e => e.Value).IsUnique();
            });


            builder.OwnsOne(u => u.Address, ad =>
            {
                ad.Property(s => s.District)
                .HasColumnName("District");

                ad.Property(s => s.City)
                .HasColumnName("City");
            });

            builder.OwnsOne(u => u.PhoneNumber, ph =>
            {
                ph.Property(s => s.Value)
                .HasColumnName("PhoneNumber")
                .IsRequired();

                ph.HasIndex(s => s.Value).IsUnique();
            });


        }
    }
}
