using LearnMUSIC.Application.SongSheets.Models;
using MediatR;

namespace LearnMUSIC.Core.Application.SongSheets.Queries.GetAllSongSheets
{
  public class GetAllSongSheetsQuery : IRequest<IEnumerable<SongSheetDto>>
  {
    public long UserId { get; set; }
  }
}
