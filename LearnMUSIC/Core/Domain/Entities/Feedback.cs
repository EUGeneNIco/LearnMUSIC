using LearnMUSIC.Core.Domain.Entities.Base;

namespace LearnMUSIC.Core.Domain.Entities
{
  public class Feedback : EntityBase
  {
    public string Content { get; set; }

    public string Subject { get; set; }

    public long UserId { get; set; }

    public virtual User User { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsServed { get; set; }

    public DateTime ServedOn { get; set; }
  }
}
