using LearnMUSIC.Common.Common;
using LearnMUSIC.Core.Application._Exceptions;
using LearnMUSIC.Core.Application._Interfaces;
using MediatR;

namespace LearnMUSIC.Core.Application.SongSheets.Commands.DeleteSongSheet
{
  public class DeleteSongSheetCommandHandler : IRequestHandler<DeleteSongSheetCommand, Unit>
  {
    private readonly IAppDbContext dbContext;
    private readonly IDateTime dateTime;

    public DeleteSongSheetCommandHandler(IAppDbContext dbContext, IDateTime dateTime)
    {
      this.dbContext = dbContext;
      this.dateTime = dateTime;
    }

    public async Task<Unit> Handle(DeleteSongSheetCommand request, CancellationToken cancellationToken)
    {
      var entity = await this.dbContext.SongSheets.FindAsync(request.Id);

      if(entity == null)
      {
        throw new NotFoundException("Songsheet not found.");
      }
      if (entity.IsDeleted)
      {
        throw new AlreadyDeletedException("Songsheet already deleted.");
      }

      //Delete
      var deletedOn = this.dateTime.Now;
      entity.IsDeleted = true;
      entity.ModifiedOn = deletedOn;

      await this.dbContext.SaveChangesAsync(cancellationToken);

      return Unit.Value;
    }
  }
}
