using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Users;
using Infrastructure.Persistance.Configuration;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Persistance
{
    public class MultiTenencyDbContext : DbContext
    {
        public MultiTenencyDbContext(DbContextOptions<MultiTenencyDbContext> options) : base(options)
        {

        }

        public DbSet<Users> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MultiTenencyDbContext).Assembly);
        }
    }
}
