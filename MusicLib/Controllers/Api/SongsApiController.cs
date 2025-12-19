using Microsoft.AspNetCore.Mvc;
using MusicLib.BLL.DTO;
using MusicLib.BLL.Interfaces;
using MusicLib.BLL.Infrastructure;

namespace MusicLib.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SongsApiController : ControllerBase
    {
        private readonly ISongService _songService;
        private readonly ILogger<SongsApiController> _logger;

        public SongsApiController(ISongService songService, ILogger<SongsApiController> logger)
        {
            _songService = songService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SongDTO>>> GetSongs([FromQuery] string? sortOrder = null)
        {
            try
            {
                IEnumerable<SongDTO> songs;
                if (!string.IsNullOrEmpty(sortOrder))
                {
                    songs = await _songService.GetSortedItemsAsync(sortOrder);
                }
                else
                {
                    songs = await _songService.GetSongs();
                }
                return Ok(songs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting songs");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SongDTO>> GetSong(int id)
        {
            try
            {
                var song = await _songService.GetSong(id);
                return Ok(song);
            }
            catch (ValidationException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting song {Id}", id);
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpPost]
        public async Task<ActionResult<SongDTO>> CreateSong([FromBody] SongDTO songDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _songService.CreateSong(songDto);
                return CreatedAtAction(nameof(GetSong), new { id = songDto.Id }, songDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating song");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSong(int id, [FromBody] SongDTO songDto)
        {
            if (id != songDto.Id)
            {
                return BadRequest(new { error = "ID mismatch" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _songService.UpdateSong(songDto);
                return NoContent();
            }
            catch (ValidationException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating song {Id}", id);
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            try
            {
                await _songService.DeleteSong(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting song {Id}", id);
                return StatusCode(500, new { error = "Internal server error" });
            }
        }
    }
}
