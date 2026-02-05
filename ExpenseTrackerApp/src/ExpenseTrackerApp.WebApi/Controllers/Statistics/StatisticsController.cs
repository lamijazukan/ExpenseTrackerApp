using AutoMapper;
using ExpenseTrackerApp.Application.Statistics.Interfaces.Application;
using ExpenseTrackerApp.Contracts.Statistics;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerApp.WebApi.Controllers.Statistics;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
public class StatisticsController : ApiControllerBase
{
    private readonly IStatisticService _statisticsService;
    private readonly IMapper _mapper;

    public StatisticsController(
        IStatisticService statisticsService,
        IMapper mapper)
    {
        _statisticsService = statisticsService;
        _mapper = mapper;
    }

 
    [HttpGet("budget/{budgetId:int}")]
    [ProducesResponseType(typeof(GetBudgetStatusResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBudgetStatus(
        [FromRoute] int budgetId,
        CancellationToken cancellationToken)
    {
        var result = await _statisticsService.GetBudgetStatusAsync(
            budgetId,
            cancellationToken);

        return result.Match(
            status => Ok(_mapper.Map<GetBudgetStatusResponse>(status)),
            Problem);
    }


    [HttpGet("monthly-savings")]
    [ProducesResponseType(typeof(GetMonthlySavingsResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMonthlySavings(
        [FromQuery] int year,
        [FromQuery] int month,
        CancellationToken cancellationToken)
    {
        var result = await _statisticsService.GetMonthlySavingsAsync(
            year,
            month,
            cancellationToken);

        return result.Match(
            savings => Ok(_mapper.Map<GetMonthlySavingsResponse>(savings)),
            Problem);
    }


    [HttpGet("category-expenses")]
    [ProducesResponseType(typeof(GetTotalExpenseByCategoryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTotalExpenseByCategory(
        [FromQuery] int categoryId,
        [FromQuery] DateOnly from,
        [FromQuery] DateOnly to,
        CancellationToken cancellationToken)
    {
        var result = await _statisticsService.GetTotalExpenseByCategoryAsync(
            categoryId,
            from,
            to,
            cancellationToken);

        return result.Match(
            total => Ok(_mapper.Map<GetTotalExpenseByCategoryResponse>(total)),
            Problem);
    }
}
