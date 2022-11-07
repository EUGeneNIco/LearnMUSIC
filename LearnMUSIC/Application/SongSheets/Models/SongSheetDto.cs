using AutoMapper;
using LearnMUSIC.Interfaces.Mapping;

namespace LearnMUSIC.Application.SongSheets.Models
{
    public class SongSheetDto : IHaveCustomMapping
    {
        public string SongTitle { get; set; }

        public string Singer { get; set; }

        public string KeySignature { get; set; }

        public void CreateMappings(Profile configuration)
        {

        }
    }
}
