using LearnMUSIC.Core.Application.Base;
using MediatR;

namespace LearnMUSIC.Application.SongSheets.Commands.CreateSongSheet
{
  public class CreateSongSheetCommand :CommandQueryBase, IRequest<long>
  {
    public long UserId { get; set; }

    public string SongTitle { get; set; }

    public string Singer { get; set; }

    public string KeySignatureId { get; set; }

    public string GenreId { get; set; }

    public string Contents { get; set; }
   }
}
