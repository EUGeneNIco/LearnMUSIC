using MediatR;

namespace LearnMUSIC.Core.Application.SongSheets.Commands.DeleteSongSheet
{
  public class DeleteSongSheetCommand : IRequest<Unit>
  {
    public long Id { get; set; }
  }
}
