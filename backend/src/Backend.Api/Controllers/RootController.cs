using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers
{
    [ApiController]
    [Route("/")]
    public sealed class RootController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                service = "Unity AWS ASP.NET API",
                status = "running"
            });
        }
    }
}
