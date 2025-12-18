using Microsoft.AspNetCore.Mvc;
using ExpenseTrackerApp.Application.MessageHistory.Interfaces.Application;
using ExpenseTrackerApp.MessageHistory;
using ExpenseTrackerApp.WebApi.Mappings;

namespace ExpenseTrackerApp.WebApi.Controllers.MessageHistory;

[ApiController]
[Route("[controller]")]
public class MessageHistoryController : ApiControllerBase
{
    private readonly IMessageHistoryService _messageHistoryService;
    
    public MessageHistoryController(IMessageHistoryService messageHistoryService)
    {
        _messageHistoryService = messageHistoryService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetMessageHistory([FromQuery] GetMessageHistoryRequest request)
    {
        var result = await _messageHistoryService.GetMessageHistoryAsync(
            startTime: request.StartDate, 
            endTime: request.EndDate, 
            HttpContext.RequestAborted);

        return result.Match(
            configs => Ok(configs.ToResponse()),
            Problem);
    }
}