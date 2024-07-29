using Microsoft.EntityFrameworkCore;
using Model;

namespace Data
{
    public class TransactionContext : DbContext
    {
        public DbSet<Transactions> Transactions { get; set; }

        public TransactionContext(DbContextOptions<TransactionContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transactions>()
                .ToContainer("Transactions")
                .HasPartitionKey(t => t.PartitionKey);

            modelBuilder.Entity<Transactions>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();

        }
    }
}
