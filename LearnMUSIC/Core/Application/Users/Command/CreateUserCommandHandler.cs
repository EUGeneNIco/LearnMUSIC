using LearnMusic.Core.Domain.Enumerations;
using LearnMUSIC.Common.Common;
using LearnMUSIC.Common.Helper;
using LearnMUSIC.Core.Application._Exceptions;
using LearnMUSIC.Core.Application._Interfaces;
using LearnMUSIC.Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnMUSIC.Core.Application.Users.Command
{
  public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, long>
  {
    private readonly IAppDbContext dbContext;
    private readonly IDateTime dateTime;

    public CreateUserCommandHandler(IAppDbContext dbContext, IDateTime dateTime)
    {
      this.dbContext = dbContext;
      this.dateTime = dateTime;
    }

    public async Task<long> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
      if(this.dbContext.Users.Any(x => x.UserName == request.Username.Trim() && !x.IsDeleted))
      {
        throw new DuplicateException("Username already exists.");
      }

      //Add User
      var createdOn = this.dateTime.Now;

      var user = new User
      {
        UserName = request.Username,
        PasswordHash = PasswordHelper.Hash(request.Password),

        FirstName = request.FirstName.Trim(),
        LastName = request.LastName.Trim(),
        Email = request.Email.Trim(),

        CreatedOn = createdOn,
      };

      this.dbContext.Users.Add(user);

      this.dbContext.SaveChanges();

      this.GrantModuleAccess(user.Id);

      return user.Id;
    }

    public void GrantModuleAccess(long userId)
    {
      var modules = this.dbContext.Modules
        .Where(x => x.Category == ModuleCategory.Usual)
        .ToList();

      //Add User Module Access
      foreach (var module in modules)
      {
        var moduleAccess = new UserModuleAccess
        {
          UserId = userId,
          ModuleId = module.Id,
          HasAccess = true,
        };

        this.dbContext.UserModuleAccesses.Add(moduleAccess);
      }

      this.dbContext.SaveChanges();
    }
  }
}
