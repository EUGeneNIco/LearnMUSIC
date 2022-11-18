using AutoMapper;
using LearnMUSIC.Core.Application._Interfaces.Mapping;
using LearnMUSIC.Core.Domain.Entities;

namespace LearnMUSIC.Core.Application.CodeListValues.Models
{
  public class CodeListValueDto : IHaveCustomMapping
  {
    public long Id { get; set; }

    public string Name { get; set; }

    public string Type { get; set; }

    public void CreateMappings(Profile configuration)
    {
      configuration.CreateMap<CodeListValue, CodeListValueDto>();
    }
  }
}
