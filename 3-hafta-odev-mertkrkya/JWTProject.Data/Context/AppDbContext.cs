using JWTProject.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace JWTProject.Data.Context
{
    public class AppDbContext : DbContext
    {
        private static string _connectionstring;

        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<AccountRefreshToken> AccountRefreshTokens { get; set; }

        public static void SetContextConnectionString(string connectionstring)
        {
            _connectionstring = connectionstring;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseNpgsql(_connectionstring);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.ToTable("account");
            });
            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.ToTable("person");
                entity.HasOne(r => r.Account).WithMany(r => r.People).HasForeignKey(r => r.AccountId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<AccountRefreshToken>(entity =>
            {
                entity.HasKey(r => r.AccountId);
                entity.ToTable("accountRefreshToken");
            });
        }
    }
}