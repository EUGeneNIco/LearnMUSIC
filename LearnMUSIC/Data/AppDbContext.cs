using LearnMUSIC.Entities;
using LearnMUSIC.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LearnMUSIC.Data
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public DbSet<SongSheet> SongSheets { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
