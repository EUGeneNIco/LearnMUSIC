using AutoMapper;
using LearnMUSIC.Application.SongSheets.Models;
using LearnMUSIC.Application.SongSheets.Queries.GetSongSheetById;
using LearnMUSIC.Core.Application._Exceptions;
using LearnMUSIC.Core.Application._Interfaces;
using MediatR;

namespace LearnMUSIC.Core.Application.SongSheets.Queries.GetSongSheetById
{
  public class GetSongSheetByIdQueryHandler : IRequestHandler<GetSongSheetByIdQuery, SongSheetDto>
  {
    private readonly IAppDbContext dbContext;
    private readonly IMapper mapper;

    public GetSongSheetByIdQueryHandler(IAppDbContext dbContext, IMapper mapper)
    {
      this.dbContext = dbContext;
      this.mapper = mapper;
    }

    public async Task<SongSheetDto> Handle(GetSongSheetByIdQuery request, CancellationToken cancellationToken)
    {
      var query = await this.dbContext.SongSheets.FindAsync(request.Id);

      if(query == null)
      {
        throw new NotFoundException("Song sheet not found.");
      }

      if (query.IsDeleted)
      {
        throw new AlreadyDeletedException("Song sheet already deleted.");
      }

      return this.mapper.Map<SongSheetDto>(query);
    }
  }
}
