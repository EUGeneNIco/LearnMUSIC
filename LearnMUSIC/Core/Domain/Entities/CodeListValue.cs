using LearnMUSIC.Core.Domain.Entities.Base;

namespace LearnMUSIC.Core.Domain.Entities
{
  public class CodeListValue : IEntity
  {
    public long Id { get; set; }

    public string Name { get; set; }

    public string Type { get; set; }
  }
}
