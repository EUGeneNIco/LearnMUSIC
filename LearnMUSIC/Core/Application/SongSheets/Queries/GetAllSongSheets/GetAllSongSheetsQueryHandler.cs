using AutoMapper;
using LearnMUSIC.Application.SongSheets.Models;
using LearnMUSIC.Core.Application._Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnMUSIC.Core.Application.SongSheets.Queries.GetAllSongSheets
{
  public class GetAllSongSheetsQueryHandler : IRequestHandler<GetAllSongSheetsQuery, IEnumerable<SongSheetDto>>
  {
    private readonly IAppDbContext dbContext;
    private readonly IMapper mapper;

    public GetAllSongSheetsQueryHandler(IAppDbContext dbContext, IMapper mapper)
    {
      this.dbContext = dbContext;
      this.mapper = mapper;
    }

    public async Task<IEnumerable<SongSheetDto>> Handle(GetAllSongSheetsQuery request, CancellationToken cancellationToken)
    {
      var query = await this.dbContext.SongSheets
        .Where(x => x.UserId == request.UserId && !x.IsDeleted)
        .ToListAsync(cancellationToken);

      return this.mapper.Map<IEnumerable<SongSheetDto>>(query);
    }
  }
}
