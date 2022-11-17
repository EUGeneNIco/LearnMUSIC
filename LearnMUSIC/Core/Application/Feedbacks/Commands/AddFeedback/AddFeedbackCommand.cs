using MediatR;

namespace LearnMUSIC.Core.Application.Feedbacks.Commands.AddFeedback
{
  public class AddFeedbackCommand : IRequest<long>
  {
    public long UserId { get; set; }

    public string Subject { get; set; }

    public string Content { get; set; }
  }
}
