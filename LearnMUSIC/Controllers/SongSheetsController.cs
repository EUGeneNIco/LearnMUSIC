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

namespace LearnMUSIC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongSheetsController : ControllerBase
    {
        private readonly SongSheetDbContext dbContext;

        public SongSheetsController(SongSheetDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //Create
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateSongSheetCommand command)
        {
            try
            {
                var songSheet = this.dbContext.SongSheets.FirstOrDefault(x => x.SongTitle.ToUpper() == command.SongTitle.ToUpper().Trim()
                       && x.Singer.ToUpper() == command.Singer.ToUpper().Trim()
                       && !x.IsDeleted);

                if (songSheet != null)
                {
                    throw new Exception("Same song is existing");
                }

                var entity = new SongSheet
                {
                    SongTitle = command.SongTitle.Trim(),
                    Singer = command.Singer.Trim(),
                    KeySignature = command.KeySignature.Trim(),
                    Contents = command.Contents.Trim(),
                    IsDeleted = false
                };

                await this.dbContext.SongSheets.AddAsync(entity);
                await this.dbContext.SaveChangesAsync();

                return Ok(entity.Id);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //Delete
        [HttpGet("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var songSheet = this.dbContext.SongSheets.FirstOrDefault(x => x.Id == id);

                if (songSheet == null)
                {
                    throw new Exception("No data found.");
                }

                if (songSheet.IsDeleted)
                {
                    throw new Exception("Already deleted!");
                }

                songSheet.IsDeleted = true;
                await this.dbContext.SaveChangesAsync();

                return Ok(songSheet.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //Update
        [HttpPost("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] UpdateSongSheetCommand command)
        {
            try
            {
                var songSheet = this.dbContext.SongSheets.FirstOrDefault(x => x.Id == command.Id 
                                    && !x.IsDeleted);

                if (songSheet == null)
                {
                    throw new Exception("No song sheet found.");
                }

                //var modifiedOn = this.dateTime.Now;
                songSheet.SongTitle = command.SongTitle;
                songSheet.Singer = command.Singer;
                songSheet.KeySignature = command.KeySignature;
                songSheet.Contents = command.Contents;
                //songSheet.ModifiedOn = modifiedOn;

                await this.dbContext.SaveChangesAsync();

                return Ok(songSheet.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //Get All
        [HttpGet("getAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var songSheets = await this.dbContext.SongSheets.Where(x => !x.IsDeleted)
                                    .ToListAsync();

                return Ok(songSheets);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetById(long id)
        {
            try
            {
                var song = await this.dbContext.SongSheets.FirstOrDefaultAsync(x => x.Id == id);

                if(song == null)
                {
                    throw new Exception("No song found.");
                }

                return Ok(song);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
