using AutoMapper;
using LearnMUSIC.Common.Common;
using LearnMUSIC.Core.Application._Exceptions;
using LearnMUSIC.Core.Application._Interfaces;
using MediatR;

namespace LearnMUSIC.Application.SongSheets.Commands.UpdateSongSheet
{
  public class UpdateSongSheetCommandHandler : IRequestHandler<UpdateSongSheetCommand, long>
  {
    private readonly IAppDbContext dbContext;
    private readonly IDateTime dateTime;

    public UpdateSongSheetCommandHandler(IAppDbContext dbContext, IDateTime dateTime)
    {
      this.dbContext = dbContext;
      this.dateTime = dateTime;
    }

    public async Task<long> Handle(UpdateSongSheetCommand request, CancellationToken cancellationToken)
    {
      var entity = await this.dbContext.SongSheets.FindAsync(request.Id);

      if(entity is null)
      {
        throw new NotFoundException("Song sheet not found.");
      }

      //Update
      var modifiedOn = this.dateTime.Now;

      entity.SongTitle = request.SongTitle;
      entity.Singer = request.Singer;
      entity.KeySignature = request.KeySignature;
      entity.Contents = request.Contents;

      entity.ModifiedOn = modifiedOn;

      await this.dbContext.SaveChangesAsync(cancellationToken);

      return entity.Id;
    }
  }
}
