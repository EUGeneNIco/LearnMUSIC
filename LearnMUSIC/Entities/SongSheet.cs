using LearnMUSIC.Entities.Base;

namespace LearnMUSIC.Entities
{
    public class SongSheet : EntityBase
    {
        public string SongTitle { get; set; }

        public string Singer { get; set; }

        public string KeySignature { get; set; }

        public string Contents { get; set; }

        public bool IsDeleted { get; set; }
    }
}
