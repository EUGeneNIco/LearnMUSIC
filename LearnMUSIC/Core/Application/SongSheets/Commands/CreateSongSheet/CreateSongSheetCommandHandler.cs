using LearnMusic.Core.Domain.Enumerations;
using LearnMUSIC.Common.Common;
using LearnMUSIC.Common.Helper;
using LearnMUSIC.Core.Application._Exceptions;
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
      var user = this.dbContext.Users.Find(request.UserId);

      if(user is null)
      {
        throw new NotFoundException("User not found.");
      }

      if(this.dbContext.SongSheets.Any(x => x.SongTitle.ToUpper() == request.SongTitle.ToUpper().Trim()
                  && x.Singer.ToUpper() == request.Singer.ToUpper().Trim()
                  && !x.IsDeleted && x.UserId == user.Id))
      {
          throw new DuplicateException("Song sheet with the same title and singer is existing.");
      }

      var key = this.dbContext.CodeListValues
        .SingleOrDefault(x => x.Id == request.KeySignatureId.ConvertToLong()
                  && x.Type == CodeListType.KeySignature);

      if (key is null)
      {
        throw new NotFoundException("Key signature not found.");
      }

      var genre = this.dbContext.CodeListValues
        .SingleOrDefault(x => x.Id == request.GenreId.ConvertToLong()
                  && x.Type == CodeListType.Genre);

      if (genre is null)
      {
        throw new NotFoundException("Genre not found.");
      }

      var createdOn = this.dateTime.Now;
      var entity = new SongSheet
      {
          SongTitle = request.SongTitle.Trim(),
          Singer = request.Singer.Trim(),
          KeySignatureId = key.Id,
          GenreId = genre.Id,
          Contents = request.Contents.Trim(),
          IsDeleted = false,
          UserId = user.Id,

          CreatedOn = createdOn,
      };

      this.dbContext.SongSheets.Add(entity);
      await this.dbContext.SaveChangesAsync(cancellationToken);

      return entity.Id;
    }
  }
}
