using LearnMUSIC.Core.Domain.Entities.Base;

namespace LearnMUSIC.Core.Domain.Entities
{
  public class Module : IEntity
  {
    public long Id { get; set; }

    public string Name { get; set; }

    public string Category { get; set; }
  }
}
