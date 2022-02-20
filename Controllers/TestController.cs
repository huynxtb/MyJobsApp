using Microsoft.AspNetCore.Mvc;

namespace MyJobsApp.Controllers
{
    [Route("test")]
    public class TestController : Controller
    {
        [HttpGet]
        [Route("400")]
        public IActionResult Test400()
        {
            return BadRequest("test 400");
        }
        
        [HttpPost]
        [Route("401")]
        public IActionResult Test401()
        {
            return Unauthorized("test 401");
        }
        
        [Route("403")]
        public IActionResult Test403()
        {
            return Forbid("test 403");
        }
        
        [Route("404")]
        public IActionResult Test404()
        {
            return NotFound("test 404");
        }
        
        [Route("500")]
        public IActionResult Test500()
        {
            return StatusCode(500);
        }
    }
}