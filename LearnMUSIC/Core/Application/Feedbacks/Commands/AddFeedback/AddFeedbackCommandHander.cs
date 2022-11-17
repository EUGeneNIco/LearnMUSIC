using LearnMUSIC.Common.Common;
using LearnMUSIC.Core.Application._Exceptions;
using LearnMUSIC.Core.Application._Interfaces;
using LearnMUSIC.Core.Domain.Entities;
using MediatR;

namespace LearnMUSIC.Core.Application.Feedbacks.Commands.AddFeedback
{
  public class AddFeedbackCommandHander : IRequestHandler<AddFeedbackCommand, long>
  {
    private readonly IAppDbContext dbContext;
    private readonly IDateTime dateTime;

    public AddFeedbackCommandHander(IAppDbContext dbContext, IDateTime dateTime)
    {
      this.dbContext = dbContext;
      this.dateTime = dateTime;
    }

    public async Task<long> Handle(AddFeedbackCommand request, CancellationToken cancellationToken)
    {
      var maxSameFB = 5m;
      var dateNow = this.dateTime.Now;

      if(!this.dbContext.Users.Any(x => x.Id == request.UserId))
      {
        throw new NotFoundException("User not found.");
      }

      var query = this.dbContext.Feedbacks
        .Where(x => x.Subject == request.Subject
            && x.UserId == request.UserId && !x.IsServed
            && !x.IsDeleted)
        .OrderBy(x => x.CreatedOn).ToList();

      if (query.Any())
      {
        if (query.Count > maxSameFB)
        {
          throw new Exception("User has sent the maximum allowable number of request with the same subject.");
        }
        if (query[0].CreatedOn.AddDays(1) > dateNow && query.Count >= 2)
        {
          throw new Exception("User has sent the maximum allowable number of request for today.");
        }
      }

      //Add new record
      var entity = new Feedback
      {
        UserId = request.UserId,
        Subject = request.Subject,
        Content = request.Content,

        CreatedOn = dateNow,
      };

      this.dbContext.Feedbacks.Add(entity);

      await this.dbContext.SaveChangesAsync(cancellationToken);

      return entity.Id;
    }
  }
}
