using AutoMapper;
using ExpenseTrackerApp.Application.Transactions.Interfaces.Application;
using ExpenseTrackerApp.Contracts.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerApp.WebApi.Controllers.Transactions;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
public class TransactionsController : ApiControllerBase
{
    private readonly ITransactionService _transactionService;
    private readonly IMapper _mapper;

    public TransactionsController(ITransactionService transactionService, IMapper mapper)
    {
        _transactionService = transactionService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetTransactionsResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTransactions(CancellationToken cancellationToken)
    {
        var result = await _transactionService.GetAllTransactionsAsync(cancellationToken);

        return result.Match(
            success => Ok(_mapper.Map<GetTransactionsResponse>(success)),
            Problem);
    }

    [HttpGet("{transactionId:int}")]
    [ProducesResponseType(typeof(TransactionResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTransactionById(
        [FromRoute] int transactionId,
        CancellationToken cancellationToken)
    {
        var result = await _transactionService.GetTransactionByIdAsync(transactionId, cancellationToken);

        return result.Match(
            transaction => Ok(transaction),
            Problem);
    }

    [HttpPost]
    [ProducesResponseType(typeof(TransactionResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateTransaction(
        [FromBody] CreateTransactionRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _transactionService.CreateTransactionAsync(
            request.PaidDate,
            request.Store,
            request.TotalAmount,
            request.PaymentMethod,
            cancellationToken);

        return result.Match(
            transaction => CreatedAtAction(
                nameof(GetTransactionById),
                new { transactionId = transaction.TransactionId },
                _mapper.Map<TransactionResponse>(transaction)),
            Problem);
    }

    [HttpPatch("{transactionId:int}")]
    [ProducesResponseType(typeof(TransactionResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateTransaction(
        [FromRoute] int transactionId,
        [FromBody] UpdateTransactionRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _transactionService.UpdateTransactionAsync(
            transactionId,
            request.PaidDate,
            request.Store,
            request.TotalAmount,
            request.PaymentMethod,
            cancellationToken);

        return result.Match(
            transaction => Ok(_mapper.Map<TransactionResponse>(transaction)),
            Problem);
    }

    [HttpDelete("{transactionId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteTransaction(
        [FromRoute] int transactionId,
        CancellationToken cancellationToken)
    {
        var result = await _transactionService.DeleteTransactionAsync(transactionId, cancellationToken);

        return result.Match(
            _ => NoContent(),
            Problem);
    }
}
