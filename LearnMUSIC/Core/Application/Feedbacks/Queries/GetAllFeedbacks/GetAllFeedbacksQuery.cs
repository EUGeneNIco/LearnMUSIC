using LearnMUSIC.Core.Application.Feedbacks.Models;
using MediatR;

namespace LearnMUSIC.Core.Application.Feedbacks.Queries.GetAllFeedbacks
{
  public class GetAllFeedbacksQuery : IRequest<IEnumerable<FeedbackDto>>
  {
  }
}
