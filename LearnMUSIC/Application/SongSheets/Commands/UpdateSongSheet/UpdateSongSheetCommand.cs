using MediatR;

namespace LearnMUSIC.Application.SongSheets.Commands.UpdateSongSheet
{
    public class UpdateSongSheetCommand
    {
        public long Id { get; set; }

        public string SongTitle { get; set; }

        public string Singer { get; set; }

        public string KeySignature { get; set; }

        public string Contents { get; set; }
    }
}
