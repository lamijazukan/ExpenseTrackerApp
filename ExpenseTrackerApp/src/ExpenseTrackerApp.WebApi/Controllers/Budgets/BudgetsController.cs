using AutoMapper;
using ExpenseTrackerApp.Application.Budgets.Interfaces.Application;
using ExpenseTrackerApp.Contracts.Budgets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ExpenseTrackerApp.WebApi.Controllers.Budgets;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
public class BudgetsController: ApiControllerBase
{
    private readonly IBudgetService _budgetService;
    private readonly IMapper _mapper;

    public BudgetsController(IBudgetService budgetService, IMapper mapper)
    {
        _budgetService = budgetService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetBudgetsResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBudgets(CancellationToken cancellationToken)
    {
        var result = await _budgetService.GetAllBudgetsAsync(cancellationToken);

        return result.Match(
            success => Ok(_mapper.Map<GetBudgetsResponse>(success)),
            Problem);
    }

    [HttpGet("{budgetId:int}")]
    [ProducesResponseType(typeof(BudgetResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBudgetById(
        [FromRoute] int budgetId,
        CancellationToken cancellationToken)
    {
        var result = await _budgetService.GetBudgetByIdAsync(budgetId, cancellationToken);
        
        return result.Match(
            budget => Ok(budget),
            Problem);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(BudgetResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateBudget(
        [FromBody] CreateBudgetRequest request,
        CancellationToken cancellationToken)
    {
        

        var result = await _budgetService.CreateBudgetAsync(
            request.CategoryId,
            request.Amount,
            request.StartDate,
            request.EndDate,
            cancellationToken);

        return result.Match(
            budget => CreatedAtAction(
                nameof(GetBudgetById),
                new { budgetId = budget.BudgetId },
                _mapper.Map<BudgetResponse>(budget)),
            Problem);
    }
    
    [HttpPatch("{budgetId:int}")]
    [ProducesResponseType(typeof(BudgetResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateBudget(
        [FromRoute]int budgetId,
        [FromBody] UpdateBudgetRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _budgetService.UpdateBudgetAsync(
            budgetId,
            request.Amount,
            request.StartDate,
            request.EndDate,
            cancellationToken);

        return result.Match(
            budget => Ok(_mapper.Map<BudgetResponse>(budget)),
            Problem);
    }

    
    [HttpDelete("{budgetId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteBudget(
        [FromRoute ]int budgetId,
        CancellationToken cancellationToken)
    {
        var result = await _budgetService.DeleteBudgetAsync(budgetId, cancellationToken);

        return result.Match(
            _ => NoContent(),
            Problem);
    }

}