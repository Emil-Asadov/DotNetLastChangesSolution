using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MinimalAPIController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("Get-Controller-Api")]
        public IActionResult Get()
        {
            return Ok("Hello world");
        }
    }
}
