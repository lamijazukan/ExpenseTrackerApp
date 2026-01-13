using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerApp.WebApi.Controllers;

[ApiController]
[Route("health")]
//[ApiExplorerSettings(IgnoreApi = false)] => this is used for enabling endpoint appearance
public class HealthController : ApiControllerBase
{
  
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        return Ok(new
        {
            status = "healthy",
            timestamp = DateTime.UtcNow,
            version = "1.0.0"
        });
    }
}