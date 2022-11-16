using LearnMUSIC.Core.Domain.Entities.Base;

namespace LearnMUSIC.Core.Domain.Entities
{
  public class UserModuleAccess : IEntity
  {
    public long Id { get ; set; }

    public long UserId { get; set; }

    public virtual User User { get; set; }

    public long ModuleId { get; set; }

    public virtual Module Module { get; set; }

    public bool HasAccess { get; set; }

    public T ShallowCopy<T>() where T : UserModuleAccess
    {
      return (T)(MemberwiseClone());
    }
  }
}
