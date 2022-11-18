using LearnMusic.Core.Domain.Enumerations;
using LearnMUSIC.Common.Common;
using LearnMUSIC.Common.Helper;
using LearnMUSIC.Core.Application._Interfaces;
using LearnMUSIC.Core.Domain.Entities;
using MediatR;

namespace LearnMUSIC.Application.SongSheets.Commands.CreateSongSheet
{
  public class CreateSongSheetCommandHandler : IRequestHandler<CreateSongSheetCommand, long>
  {
    private readonly IAppDbContext dbContext;
    private readonly IDateTime dateTime;

    public CreateSongSheetCommandHandler(IAppDbContext dbContext, IDateTime dateTime)
    {
      this.dbContext = dbContext;
      this.dateTime = dateTime;
    }

    public async Task<long> Handle(CreateSongSheetCommand request, CancellationToken cancellationToken)
    {
      var song = this.dbContext.SongSheets.FirstOrDefault(x => x.SongTitle.ToUpper() == request.SongTitle.ToUpper().Trim() 
                  && x.Singer.ToUpper() == request.Singer.ToUpper().Trim()
                  && !x.IsDeleted);

      if(song != null)
      {
          throw new Exception("Song sheet with the same title and singer is existing.");
      }

      var key = this.dbContext.CodeListValues
        .SingleOrDefault(x => x.Id == request.KeySignature
        .ConvertToLong() && x.Type == CodeListType.KeySignature);

      if (key is null)
      {
        throw new Exception("Key signature not found.");
      }

      var createdOn = this.dateTime.Now;
      var entity = new SongSheet
      {
          SongTitle = request.SongTitle.Trim(),
          Singer = request.Singer.Trim(),
          KeySignature = key.Name,
          Contents = request.Contents.Trim(),
          IsDeleted = false,
          UserId = request.UserId,

          CreatedOn = createdOn,
      };

      this.dbContext.SongSheets.Add(entity);
      await this.dbContext.SaveChangesAsync(cancellationToken);

      return entity.Id;
    }
  }
}
