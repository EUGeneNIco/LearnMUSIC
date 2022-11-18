using MediatR;

namespace LearnMUSIC.Application.SongSheets.Commands.UpdateSongSheet
{
  public class UpdateSongSheetCommand : IRequest<long>
  {
    public long Id { get; set; }

    public string SongTitle { get; set; }

    public string Singer { get; set; }

    public string KeySignatureId { get; set; }

    public string GenreId { get; set; }

    public string Contents { get; set; }
  }
}
