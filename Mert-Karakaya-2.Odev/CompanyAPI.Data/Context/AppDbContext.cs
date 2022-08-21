using CompanyAPI.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace CompanyAPI.Data.Context
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

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Folder> Folders { get; set; }

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
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(r => r.EmpId);
                entity.ToTable("employee");
                entity.HasOne(r => r.Department).WithMany(r => r.Employees).HasForeignKey(r => r.DeptId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.Property(r => r.EmpId).HasColumnName("empid");
                entity.Property(r => r.EmpName).HasColumnName("empname");
                entity.Property(r => r.DeptId).HasColumnName("deptid");
            });
            modelBuilder.Entity<Folder>(entity =>
            {
                entity.ToTable("folder");
                entity.HasOne(r => r.Employee).WithMany(r => r.Folders).HasForeignKey(r => r.EmpId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.Property(r => r.FolderId).HasColumnName("folderid");
                entity.Property(r => r.AccessType).HasColumnName("accesstype");
                entity.Property(r => r.EmpId).HasColumnName("empid");
            });
        }
    }
}