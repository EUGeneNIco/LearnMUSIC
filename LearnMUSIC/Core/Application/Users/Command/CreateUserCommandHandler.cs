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
      if(this.dbContext.Users.Any(x => x.Username == request.Username.Trim() && !x.IsDeleted))
      {
        throw new DuplicateException("Username already exists.");
      }

      //Add
      var createdOn = this.dateTime.Now;
      var user = new User
      {
        Username = request.Username,
        PasswordHash = PasswordHelper.Hash(request.Password),

        FirstName = request.FirstName.Trim(),
        LastName = request.LastName.Trim(),

        CreatedOn = createdOn,
      };

      this.dbContext.Users.Add(user);

      await this.dbContext.SaveChangesAsync(cancellationToken);

      return user.Id;
    }
  }
}
