using AFPMBAI.CLAIMS.DbUpdate;
using LearnMUSIC.Core.Application._Interfaces;
using MediatR;

namespace LearnMUSIC.Core.Application.DbUpdaters.Command.RunDbUpdate
{
  public class RunDbUpdateCommandHandler : IRequestHandler<RunDbUpdateCommand, Unit>
  {
    private readonly IAppDbContext dbContext;

    public RunDbUpdateCommandHandler(IAppDbContext dbContext)
    {
      this.dbContext = dbContext;
    }

    public async Task<Unit> Handle(RunDbUpdateCommand request, CancellationToken cancellationToken)
    {
      var dbUpdate = new DbUpdater();

      dbUpdate.Start();

      return Unit.Value;
    }
  }
}
