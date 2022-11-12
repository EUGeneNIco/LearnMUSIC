using AutoMapper;
using LearnMUSIC.Core.Application._Interfaces.Mapping;
using LearnMUSIC.Core.Domain.Entities;

namespace LearnMUSIC.Application.SongSheets.Models
{
  public class SongSheetDto : IHaveCustomMapping
  {
    public long Id { get; set; }

    public string SongTitle { get; set; }

    public string Singer { get; set; }

    public string KeySignature { get; set; }

    public string Contents { get; set; }

    public void CreateMappings(Profile configuration)
    {
      configuration.CreateMap<SongSheet, SongSheetDto>();
    }
  }
}
