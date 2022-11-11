using AutoMapper;
using LearnMUSIC.Application.SongSheets.Commands.CreateSongSheet;
using LearnMUSIC.Application.SongSheets.Queries.GetSongSheetById;
using LearnMUSIC.Application.SongSheets.Commands.UpdateSongSheet;
using LearnMUSIC.Application.SongSheets.Models;
using LearnMUSIC.Application.SongSheets.Queries.GetSongSheetById;
using LearnMUSIC.Common;
using LearnMUSIC.Data;
using LearnMUSIC.Entities;
using LearnMUSIC.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearnMUSIC.Application.SongSheets.Queries.GetSongSheetById;
using LearnMUSIC.Controllers;
using LearnMUSIC.Core.Application._Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace LearnMUSIC.Interface.WebAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class SongSheetsController : ApiControllerBase
  {
    //private readonly IAppDbContext dbContext;

    //public SongSheetsController(IAppDbContext dbContext)
    //{
    //  this.dbContext = dbContext;
    //}

    //Create
    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Create([FromBody] CreateSongSheetCommand command)
    {
      try
      {
        var data = await this.Mediator.Send(command);

        return new JsonResult(data);
      }
      catch (NotFoundException ex)
      {
        return BadRequest(ex.Message);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }
    ////Delete
    //[HttpGet("delete/{id}")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //public async Task<IActionResult> Delete(long id)
    //{
    //  try
    //  {
    //    var songSheet = this.dbContext.SongSheets.FirstOrDefault(x => x.Id == id);

    //    if (songSheet == null)
    //    {
    //      throw new Exception("No data found.");
    //    }

    //    if (songSheet.IsDeleted)
    //    {
    //      throw new Exception("Already deleted!");
    //    }

    //    songSheet.IsDeleted = true;
    //    await this.dbContext.SaveChangesAsync();

    //    return Ok(songSheet.Id);
    //  }
    //  catch (Exception ex)
    //  {
    //    return BadRequest(ex.Message);
    //  }
    //}
    ////Update
    //[HttpPost("update")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //public async Task<IActionResult> Update([FromBody] UpdateSongSheetCommand command)
    //{
    //  try
    //  {
    //    var songSheet = this.dbContext.SongSheets.FirstOrDefault(x => x.Id == command.Id
    //                        && !x.IsDeleted);

    //    if (songSheet == null)
    //    {
    //      throw new Exception("No song sheet found.");
    //    }

    //    //var modifiedOn = this.dateTime.Now;
    //    songSheet.SongTitle = command.SongTitle;
    //    songSheet.Singer = command.Singer;
    //    songSheet.KeySignature = command.KeySignature;
    //    songSheet.Contents = command.Contents;
    //    //songSheet.ModifiedOn = modifiedOn;

    //    await this.dbContext.SaveChangesAsync();

    //    return Ok(songSheet.Id);
    //  }
    //  catch (Exception ex)
    //  {
    //    return BadRequest(ex.Message);
    //  }
    //}
    ////Get All
    //[HttpGet("getAll")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //public async Task<IActionResult> GetAll()
    //{
    //  try
    //  {
    //    var songSheets = await this.dbContext.SongSheets.Where(x => !x.IsDeleted)
    //                        .ToListAsync();

    //    return Ok(songSheets);

    //  }
    //  catch (Exception ex)
    //  {
    //    return BadRequest(ex.Message);
    //  }
    //}

    //[HttpGet("{id}")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //public async Task<ActionResult> GetById(long id)
    //{
    //  try
    //  {
    //    var song = await this.dbContext.SongSheets.FirstOrDefaultAsync(x => x.Id == id);

    //    if (song == null)
    //    {
    //      throw new Exception("No song found.");
    //    }

    //    return Ok(song);
    //  }
    //  catch (Exception ex)
    //  {
    //    return BadRequest(ex.Message);
    //  }

    //}
  }
}
