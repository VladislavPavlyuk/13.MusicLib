using Microsoft.AspNetCore.Mvc;
using MusicLib.BLL.DTO;
using MusicLib.BLL.Interfaces;
using MusicLib.BLL.Infrastructure;

namespace MusicLib.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class GenresApiController : ControllerBase
    {
        private readonly IGenreService _genreService;
        private readonly ILogger<GenresApiController> _logger;

        public GenresApiController(IGenreService genreService, ILogger<GenresApiController> logger)
        {
            _genreService = genreService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDTO>>> GetGenres()
        {
            try
            {
                var genres = await _genreService.GetGenres();
                return Ok(genres);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting genres");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenreDTO>> GetGenre(int id)
        {
            try
            {
                var genre = await _genreService.GetGenre(id);
                return Ok(genre);
            }
            catch (ValidationException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting genre {Id}", id);
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpPost]
        public async Task<ActionResult<GenreDTO>> CreateGenre([FromBody] GenreDTO genreDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _genreService.CreateGenre(genreDto);
                return CreatedAtAction(nameof(GetGenre), new { id = genreDto.Id }, genreDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating genre");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre(int id, [FromBody] GenreDTO genreDto)
        {
            if (id != genreDto.Id)
            {
                return BadRequest(new { error = "ID mismatch" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _genreService.UpdateGenre(genreDto);
                return NoContent();
            }
            catch (ValidationException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating genre {Id}", id);
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            try
            {
                await _genreService.DeleteGenre(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting genre {Id}", id);
                return StatusCode(500, new { error = "Internal server error" });
            }
        }
    }
}
