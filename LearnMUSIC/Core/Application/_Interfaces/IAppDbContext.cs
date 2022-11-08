
using LearnMUSIC.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LearnMUSIC.Core.Application._Interfaces
{
  public interface IAppDbContext
  {
    DbSet<SongSheet> SongSheets { get; set; }

    /******************************************************************************/

    EntityEntry Remove(object entity);

    void RemoveRange(IEnumerable<object> entities);

    int SaveChanges();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
  }
}