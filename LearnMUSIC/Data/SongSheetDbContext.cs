using LearnMUSIC.Entities;
using LearnMUSIC.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LearnMUSIC.Data
{
    public class SongSheetDbContext : DbContext
    {
        public DbSet<SongSheet> SongSheets { get; set; }

        public SongSheetDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
