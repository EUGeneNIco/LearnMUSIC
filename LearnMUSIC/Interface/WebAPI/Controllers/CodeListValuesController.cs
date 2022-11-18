using LearnMUSIC.Controllers;
using LearnMUSIC.Core.Application._Exceptions;
using LearnMUSIC.Core.Application.CodeListValues.Queries.GetGenres;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnMUSIC.Interface.WebAPI.Controllers
{
  [AllowAnonymous]
  public class CodeListValuesController : ApiControllerBase
  {
    [HttpGet("getGenres")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetGenres()
    {
      try
      {
        var data = await this.Mediator.Send(new GetGenresQuery { });

        return new JsonResult(data);
      }
      catch (NotFoundException ex)
      {
        return BadRequest(ex.Message);
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex);
      }
    }
  }
}
