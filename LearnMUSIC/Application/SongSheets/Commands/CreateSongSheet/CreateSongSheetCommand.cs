using MediatR;

namespace LearnMUSIC.Application.SongSheets.Commands.CreateSongSheet
{
    public class CreateSongSheetCommand : IRequest<long>
    {
        public string SongTitle { get; set; }

        public string Singer { get; set; }

        public string KeySignature { get; set; }

        public string Contents { get; set; }
    }
}
