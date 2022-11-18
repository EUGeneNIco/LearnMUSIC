using AutoMapper;
using LearnMusic.Core.Domain.Enumerations;
using LearnMUSIC.Core.Application._Exceptions;
using LearnMUSIC.Core.Application._Interfaces;
using LearnMUSIC.Core.Application.CodeListValues.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnMUSIC.Core.Application.CodeListValues.Queries.GetGenres
{
  public class GetGenresQueryHandler : IRequestHandler<GetGenresQuery, IEnumerable<CodeListValueDto>>
  {
    private readonly IAppDbContext dbContext;
    private readonly IMapper mapper;

    public GetGenresQueryHandler(IAppDbContext dbContext, IMapper mapper)
    {
      this.dbContext = dbContext;
      this.mapper = mapper;
    }

    public async Task<IEnumerable<CodeListValueDto>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
    {
      var query = await this.dbContext.CodeListValues
        .Where(x => x.Type == CodeListType.Genre)
        .ToListAsync(cancellationToken);

      if (!query.Any())
      {
        throw new NotFoundException("Genres not found.");
      }

      return this.mapper.Map< IEnumerable<CodeListValueDto>>(query);
    }
  }
}
