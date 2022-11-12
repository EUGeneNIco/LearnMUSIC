using LearnMUSIC.Application.SongSheets.Models;
using MediatR;

namespace LearnMUSIC.Application.SongSheets.Queries.GetSongSheetById
{
    public class GetSongSheetByIdQuery : IRequest<SongSheetDto>
    {
        public long Id { get; set; }
    }
}
