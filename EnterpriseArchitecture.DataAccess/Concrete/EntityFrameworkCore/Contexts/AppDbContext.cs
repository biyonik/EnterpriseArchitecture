using EnterpriseArchitecture.DataAccess.Concrete.EntityFrameworkCore.Configurations;
using EnterpriseArchitecture.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseArchitecture.DataAccess.Concrete.EntityFrameworkCore.Contexts;

public class AppDbContext: DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            "Server=127.0.0.1;Port=5433;Database=EnterpriseArchAppDb;User Id=postgres;Password=12345");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}