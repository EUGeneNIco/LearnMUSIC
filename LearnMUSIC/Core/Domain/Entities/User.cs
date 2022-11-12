namespace LearnMUSIC.Core.Domain.Entities
{
    public class User
    {
        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? LastSuccessfulLogin { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsLocked { get; set; }

    }
}