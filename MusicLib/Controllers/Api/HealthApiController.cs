using Microsoft.AspNetCore.Mvc;

namespace MusicLib.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { status = "ok", message = "API is running" });
        }
    }
}
