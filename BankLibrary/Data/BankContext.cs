using BankLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace BankLibrary.Data
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options) : base(options) { }

        public DbSet<Bank> Banks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // bank entity, set PK
            modelBuilder.Entity<Bank>()
                .HasKey(b => b.BankId);
            // set FK
            modelBuilder.Entity<Bank>()
                .HasMany(b => b.Users)
                .WithOne(u => u.Bank)
                .HasForeignKey(u => u.BankId);

            // configure user entity, set PK
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);
            // set FK
            modelBuilder.Entity<User>()
               .HasMany(u => u.Accounts)
               .WithOne(a => a.Owner)
               .HasForeignKey(a => a.OwnerId);

            // set PK for Account
            modelBuilder.Entity<Account>()
                .HasKey(a => a.AccountId);
            // set FK
            modelBuilder.Entity<Account>()
                .HasMany(a => a.LinkedCards)
                .WithOne(c => c.LinkedAccount)
                .HasForeignKey(c => c.LinkedAccountId);

            // PK for Card
            modelBuilder.Entity<Card>()
                .HasKey(c => c.CardId);

            // PK for Transaction
            modelBuilder.Entity<Transaction>()
                .HasKey(t => t.TransactionId);
        }
    }
}
