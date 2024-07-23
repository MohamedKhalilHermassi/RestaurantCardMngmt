using Microsoft.EntityFrameworkCore;
using RM.User.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace RM.User.Data.Data
{
    public class UserContext:DbContext
    {
          public DbSet<Utilisateur> Users { get; set; }

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Utilisateur>().ToContainer("Users").HasPartitionKey(u => u.PartitionKey);
            modelBuilder.Entity<Utilisateur>()
               .Property(c => c.Id)
               .ValueGeneratedOnAdd();

        }
    }
}
