using LearnMUSIC.Common;
using LearnMUSIC.Core.Application._Interfaces;
using LearnMUSIC.Entities;
using LearnMUSIC.Interfaces;
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
                throw new Exception("Same song is existing");
            }

            var entity = new SongSheet
            {
                SongTitle = request.SongTitle.Trim(),
                Singer = request.Singer.Trim(),
                KeySignature = request.KeySignature.Trim(),
                Contents = request.Contents.Trim(),
                IsDeleted = false
            };

            this.dbContext.SongSheets.Add(entity);
            await this.dbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
