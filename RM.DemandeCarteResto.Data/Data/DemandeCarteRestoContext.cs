using Microsoft.EntityFrameworkCore;
using RM.DemandeCarteResto.Model;

namespace RM.DemandeCarteResto.Data
{
    public class DemandeCarteRestoContext: DbContext
    {
        public DbSet<DemandeCarteRestaurant> DemandesCarteRestaurant { get; set; }
        public DemandeCarteRestoContext(DbContextOptions<DemandeCarteRestoContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DemandeCarteRestaurant>().ToContainer("DemandesCarteRestaurant")
                .HasPartitionKey(d => d.PartitionKey);

            modelBuilder.Entity<DemandeCarteRestaurant>()
               .Property(c => c.Id)
               .ValueGeneratedOnAdd();

            modelBuilder.Entity<DemandeCarteRestaurant>()
                .Property(c => c.UserId);
        }
    }
}
