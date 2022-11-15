using LearnMUSIC.Core.Application.Users.Models;
using MediatR;

namespace LearnMUSIC.Core.Application.Users.Queries.AuthenticateUser
{
  public class AuthenticateUserQuery : IRequest<UserClaimsDto>
  {
    public string Username { get; set; }

    public string Password { get; set; }
  }
}
