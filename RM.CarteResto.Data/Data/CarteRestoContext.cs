using Microsoft.EntityFrameworkCore;
using RM.CarteResto.Model;

namespace RM.CarteResto.Data
{
    public class CarteRestoContext: DbContext
    {
        public DbSet<CarteRestaurant> CartesRestaurant { get; set; }
        public CarteRestoContext(DbContextOptions<CarteRestoContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CarteRestaurant>().ToContainer("CartesRestaurant")
                .HasPartitionKey(c => c.PartitionKey);

            modelBuilder.Entity<CarteRestaurant>()
               .Property(c => c.Id)
               .ValueGeneratedOnAdd();

            modelBuilder.Entity<CarteRestaurant>()
                .Property(c => c.TransactionIds);

        }
    }
}
