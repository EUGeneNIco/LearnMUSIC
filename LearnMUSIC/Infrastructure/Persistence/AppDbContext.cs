using LearnMUSIC.Core.Application._Interfaces;
using LearnMUSIC.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearnMUSIC.Infrastructure.Persistence
{
  public class AppDbContext : DbContext, IAppDbContext
  {
    public DbSet<SongSheet> SongSheets { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<UserModuleAccess> UserModuleAccesses { get; set; }

    public DbSet<Module> Modules { get; set; } 

    public AppDbContext() { }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      //modelBuilder.RemovePluralizingTableNameConvention();

      modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
  }
}
