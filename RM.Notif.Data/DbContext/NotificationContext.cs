using Microsoft.EntityFrameworkCore;
using RM.Notif.Model.Entities;

namespace RM.DemandeCarteResto.Data.Data
{
    public class NotificationContext : DbContext    
    {
        public DbSet<Notification> Notifications { get; set; }
        public NotificationContext(DbContextOptions<NotificationContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Notification>().ToContainer("Notifications")
                .HasPartitionKey(n => n.PartitionKey);

            modelBuilder.Entity<Notification>()
               .Property(c => c.NotificationId)
               .ValueGeneratedOnAdd();
        

          
        }
    }
}
