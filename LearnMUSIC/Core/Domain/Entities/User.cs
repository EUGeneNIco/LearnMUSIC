using LearnMUSIC.Core.Domain.Entities.Base;

namespace LearnMUSIC.Core.Domain.Entities
{
  public class User : EntityBase
  {
    public string UserName { get; set; }

    public string PasswordHash { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public bool AccountStatus { get; set; }

    public DateTime? LastSuccessfulLogin { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsLocked { get; set; }

    public virtual ICollection<UserModuleAccess> ModuleAccesses { get; set; }

    public User()
    {
      this.ModuleAccesses = new HashSet<UserModuleAccess>();
    }

  }
}
