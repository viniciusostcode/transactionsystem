using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sistema.Data.Map;
using Sistema.Models;
using Sistema.Models.Enums;

namespace Sistema.Data
{
    public class TransactionSystemDbContext : DbContext
    {
        public TransactionSystemDbContext(DbContextOptions<TransactionSystemDbContext> options)
            : base(options)
        {

        }

        public DbSet<UserModel> Users { get; set; }

        public DbSet<TransactionModel> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new TransactionMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
