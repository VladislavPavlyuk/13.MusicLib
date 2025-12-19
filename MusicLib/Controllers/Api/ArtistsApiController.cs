using Microsoft.AspNetCore.Mvc;
using MusicLib.BLL.DTO;
using MusicLib.BLL.Interfaces;
using MusicLib.BLL.Infrastructure;

namespace MusicLib.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ArtistsApiController : ControllerBase
    {
        private readonly IArtistService _artistService;
        private readonly ILogger<ArtistsApiController> _logger;

        public ArtistsApiController(IArtistService artistService, ILogger<ArtistsApiController> logger)
        {
            _artistService = artistService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistDTO>>> GetArtists()
        {
            try
            {
                var artists = await _artistService.GetArtists();
                return Ok(artists);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting artists");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistDTO>> GetArtist(int id)
        {
            try
            {
                var artist = await _artistService.GetArtist(id);
                return Ok(artist);
            }
            catch (ValidationException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting artist {Id}", id);
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ArtistDTO>> CreateArtist([FromBody] ArtistDTO artistDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _artistService.CreateArtist(artistDto);
                return CreatedAtAction(nameof(GetArtist), new { id = artistDto.Id }, artistDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating artist");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArtist(int id, [FromBody] ArtistDTO artistDto)
        {
            if (id != artistDto.Id)
            {
                return BadRequest(new { error = "ID mismatch" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _artistService.UpdateArtist(artistDto);
                return NoContent();
            }
            catch (ValidationException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating artist {Id}", id);
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            try
            {
                await _artistService.DeleteArtist(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting artist {Id}", id);
                return StatusCode(500, new { error = "Internal server error" });
            }
        }
    }
}
