using LearnMUSIC.Application.SongSheets.Models;
using LearnMUSIC.Entities;
using MediatR;

namespace LearnMUSIC.Application.SongSheets.Queries.GetSongSheetById
{
    public class GetSongSheetByIdQuery 
    {
        public long Id { get; set; }
    }
}
