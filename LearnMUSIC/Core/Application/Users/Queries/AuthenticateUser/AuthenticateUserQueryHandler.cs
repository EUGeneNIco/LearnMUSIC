using LearnMUSIC.Common.Common;
using LearnMUSIC.Common.Helper;
using LearnMUSIC.Core.Application._Exceptions;
using LearnMUSIC.Core.Application._Interfaces;
using LearnMUSIC.Core.Application.Users.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LearnMUSIC.Core.Application.Users.Queries.AuthenticateUser
{
  public class AuthenticateUserQueryHandler : IRequestHandler<AuthenticateUserQuery, UserClaimsDto>
  {
    private readonly IAppDbContext dbContext;
    private readonly IDateTime dateTime;

    public AuthenticateUserQueryHandler(IAppDbContext dbContext, IDateTime dateTime)
    {
      this.dbContext = dbContext;
      this.dateTime = dateTime;
    }

    public async Task<UserClaimsDto> Handle(AuthenticateUserQuery request, CancellationToken cancellationToken)
    {
      var user = await this.dbContext.Users.FirstOrDefaultAsync(x => x.UserName == request.Username.Trim());
          
      if(user is null)
      {
        throw new UnauthorizedException("Unauthorized.");
      }
      if (user.IsLocked)
      {
        throw new ForbiddenException("Account Locked");
      }
      if(user.PasswordHash == PasswordHelper.Hash(request.Password))
      {
        var successfulLogin = this.dateTime.Now;
        user.LastSuccessfulLogin = successfulLogin;

        await dbContext.SaveChangesAsync(cancellationToken);

        var userClaims = new UserClaimsDto
        {
          UserGuid = user.Id,
          DisplayName = $"{user.FirstName} {user.LastName}",
          EmailAddress = user.Email
        };

        var accessibleModules = string.Join(",", user.ModuleAccesses
                                     .Where(x => x.HasAccess).Select(x => x.Module.Name));

        userClaims.Claims.Add(new Claim("AccessibleModules", accessibleModules));

        return userClaims;
      }
      else
      {
        throw new ForbiddenException("Wrong Password.");
      }
    }
  }
}
