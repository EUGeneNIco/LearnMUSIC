using LearnMUSIC.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LearnMUSIC.Core.Application._Interfaces
{
  public interface IAppDbContext
  {
    DbSet<Feedback> Feedbacks { get; set; }

    DbSet<SongSheet> SongSheets { get; set; }

    DbSet<User> Users { get; set; }

    DbSet<UserModuleAccess> UserModuleAccesses { get; set; }

    DbSet<Module> Modules { get; set; }

    /******************************************************************************/

    EntityEntry Remove(object entity);

    void RemoveRange(IEnumerable<object> entities);

    int SaveChanges();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
  }
}
