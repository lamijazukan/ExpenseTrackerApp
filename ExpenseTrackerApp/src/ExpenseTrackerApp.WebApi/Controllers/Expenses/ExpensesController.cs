using AutoMapper;
using ExpenseTrackerApp.Application.Expenses.Interfaces.Application;
using ExpenseTrackerApp.Contracts.Expenses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerApp.WebApi.Controllers.Expenses;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
public class ExpensesController : ApiControllerBase
{
    private readonly IExpenseService _expenseService;
    private readonly IMapper _mapper;

    public ExpensesController(IExpenseService expenseService, IMapper mapper)
    {
        _expenseService = expenseService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetExpensesResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetExpenses(CancellationToken cancellationToken)
    {
        var result = await _expenseService.GetAllExpensesAsync(cancellationToken);

        return result.Match(
            success => Ok(_mapper.Map<GetExpensesResponse>(success)),
            Problem);
    }

    [HttpGet("{expenseId:int}")]
    [ProducesResponseType(typeof(ExpenseResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetExpenseById(
        [FromRoute] int expenseId,
        CancellationToken cancellationToken)
    {
        var result = await _expenseService.GetExpenseByIdAsync(expenseId, cancellationToken);

        return result.Match(
            expense => Ok(expense),
            Problem);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ExpenseResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateExpense(
        [FromBody] CreateExpenseRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _expenseService.CreateExpenseAsync(
            request.TransactionId,
            request.CategoryId,
            request.Amount,
            request.ProductName,
            cancellationToken);

        return result.Match(
            expense => CreatedAtAction(
                nameof(GetExpenseById),
                new { expenseId = expense.ExpenseId },
                _mapper.Map<ExpenseResponse>(expense)),
            Problem);
    }

    [HttpPatch("{expenseId:int}")]
    [ProducesResponseType(typeof(ExpenseResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateExpense(
        [FromRoute] int expenseId,
        [FromBody] UpdateExpenseRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _expenseService.UpdateExpenseAsync(
            expenseId,
            request.TransactionId,
            request.CategoryId,
            request.Amount,
            request.ProductName,
            cancellationToken);

        return result.Match(
            expense => Ok(_mapper.Map<ExpenseResponse>(expense)),
            Problem);
    }

    [HttpDelete("{expenseId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteExpense(
        [FromRoute] int expenseId,
        CancellationToken cancellationToken)
    {
        var result = await _expenseService.DeleteExpenseAsync(expenseId, cancellationToken);

        return result.Match(
            _ => NoContent(),
            Problem);
    }
}
