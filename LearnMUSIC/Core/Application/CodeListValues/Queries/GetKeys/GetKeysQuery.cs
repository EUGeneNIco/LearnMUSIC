using LearnMUSIC.Core.Application.CodeListValues.Models;
using MediatR;

namespace LearnMUSIC.Core.Application.CodeListValues.Queries.GetKeys
{
    public class GetKeysQuery : IRequest<IEnumerable<CodeListValueDto>>
    {
    }
}
