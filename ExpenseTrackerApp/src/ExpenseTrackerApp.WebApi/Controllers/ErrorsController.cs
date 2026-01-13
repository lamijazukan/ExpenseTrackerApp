using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerApp.WebApi.Controllers;

[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorsController : ApiControllerBase
{
    [Route("/errors")]
    [HttpGet, HttpPost, HttpPut, HttpDelete, HttpPatch] // Handle all HTTP methods
    public IActionResult Error()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        
        // You could also use: HttpContext.Items[HttpContextItemKeys.Error]
        
        return Problem(
            title: "An error occurred",
            statusCode: StatusCodes.Status500InternalServerError
        );
    }
}