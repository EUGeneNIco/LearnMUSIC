using AutoMapper;
using LearnMUSIC.Core.Application._Interfaces;
using LearnMUSIC.Core.Application.Feedbacks.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnMUSIC.Core.Application.Feedbacks.Queries.GetAllFeedbacks
{
  public class GetAllFeedbacksQueryHandler : IRequestHandler<GetAllFeedbacksQuery, IEnumerable<FeedbackDto>>
  {
    private readonly IAppDbContext dbContext;
    private readonly IMapper mapper;

    public GetAllFeedbacksQueryHandler(IAppDbContext dbContext, IMapper mapper)
    {
      this.dbContext = dbContext;
      this.mapper = mapper;
    }

    public async Task<IEnumerable<FeedbackDto>> Handle(GetAllFeedbacksQuery request, CancellationToken cancellationToken)
    {
      var query = await this.dbContext.Feedbacks
        .Where(s => !s.IsServed && !s.IsDeleted)
        .ToListAsync(cancellationToken);

      return this.mapper.Map<IEnumerable<FeedbackDto>>(query);
    }
  }
}
