using Microsoft.EntityFrameworkCore;
using RM.Notif.Model.Entities;
using System.Reflection.Emit;

namespace RM.DemandeCarteResto.Data.Data
{
    public class EmailNotificationContext : DbContext
    {
        public DbSet<EmailNotification> EmailNotifications { get; set; }
        public EmailNotificationContext(DbContextOptions<EmailNotificationContext> options) : base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EmailNotification>().ToContainer("EmailNotifications")
                .HasPartitionKey(e => e.PartitionKey);

            modelBuilder.Entity<EmailNotification>()
               .Property(c => c.Id)
               .ValueGeneratedOnAdd();


        }
    }
}
