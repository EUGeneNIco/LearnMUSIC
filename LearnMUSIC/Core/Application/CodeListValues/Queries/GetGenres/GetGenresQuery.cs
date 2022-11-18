using LearnMUSIC.Core.Application.CodeListValues.Models;
using MediatR;

namespace LearnMUSIC.Core.Application.CodeListValues.Queries.GetGenres
{
    public class GetGenresQuery : IRequest<IEnumerable<CodeListValueDto>>
    {
    }
}
