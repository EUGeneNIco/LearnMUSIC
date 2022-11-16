using MediatR;

namespace LearnMUSIC.Core.Application.Users.Command
{
  public class CreateUserCommand : IRequest<long>
  {
    public string Username { get; set; }

    public string Password { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }
  }
}
