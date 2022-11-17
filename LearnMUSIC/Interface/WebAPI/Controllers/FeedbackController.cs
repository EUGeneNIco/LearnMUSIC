using LearnMUSIC.Controllers;
using LearnMUSIC.Core.Application._Exceptions;
using LearnMUSIC.Core.Application.Feedbacks.Commands.AddFeedback;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnMUSIC.Interface.WebAPI.Controllers
{
  [AllowAnonymous]
  public class FeedbackController : ApiControllerBase
  {

    [HttpPost("addFeedback")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateFeedback([FromBody] AddFeedbackCommand command)
    {
      try
      {
        var data = await this.Mediator.Send(command);

        return new JsonResult(data);
      }
      catch(NotFoundException ex)
      {
        return BadRequest(ex.Message);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

  }
}
