using LearnMUSIC.Core.Application._Interfaces;
using LearnMUSIC.Entities;
using LearnMUSIC.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LearnMUSIC.Data
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public DbSet<SongSheet> SongSheets { get; set; }

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
