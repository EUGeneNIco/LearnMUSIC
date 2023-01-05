using AutoMapper;
using LearnMUSIC.Core.Application._Interfaces.Mapping;
using LearnMUSIC.Core.Domain.Entities;

namespace LearnMUSIC.Core.Application.Feedbacks.Models
{
  public class FeedbackDto : IHaveCustomMapping
  {
    public long Id { get; set; }

    public string SenderName { get; set; }

    public string Subject { get; set; }

    public DateTime CreatedOn { get; set; }

    public void CreateMappings(Profile configuration)
    {
      configuration.CreateMap<Feedback, FeedbackDto>()
        .ForMember(dm => dm.SenderName, mo => mo.MapFrom(s => s.User.FirstName))
        .ForMember(dm => dm.CreatedOn, mo => mo.MapFrom(s => s.CreatedOn));
    }
  }
}
