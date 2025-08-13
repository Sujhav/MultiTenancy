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


            builder.OwnsMany(rt => rt.RefreshTokens, rft =>
            {
                rft.HasKey(s => s.Id);
                rft.WithOwner().HasForeignKey(rt => rt.UserId);
                rft.Property(s => s.Id).HasConversion(u => u.Value, value => RefreshTokenId.CreateNew(value));
                rft.Property(s => s.Token).HasMaxLength(200);
                rft.HasIndex(s => s.Token).IsUnique();
                rft.Property(s => s.UserId).HasConversion(id => id.Value, value => UserId.Create(value));
            });

            builder.Metadata.FindNavigation(nameof(Users.RefreshTokens))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            //builder.Navigation(s => s.RefreshTokens).UsePropertyAccessMode(PropertyAccessMode.Field).HasField("_refreshToken");



        }
    }
}
