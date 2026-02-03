using ErrorOr;
using ExpenseTrackerApp.Application.Transactions.Data;
using ExpenseTrackerApp.Domain.Entities;


namespace ExpenseTrackerApp.Application.Transactions.Interfaces.Infrastructure;

public interface ITransactionRepository
{
    Task<ErrorOr<GetTransactionsResult<Transaction>>> GetTransactionsByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken);

    Task<ErrorOr<Transaction>> GetTransactionByIdAsync(
        Guid userId,
        int transactionId,
        CancellationToken cancellationToken);

    Task<ErrorOr<Transaction>> CreateTransactionAsync(
        Transaction transaction,
        CancellationToken cancellationToken);

    Task<ErrorOr<Transaction>> UpdateTransactionAsync(
        Transaction transaction,
        CancellationToken cancellationToken);

    Task<ErrorOr<Success>> DeleteTransactionAsync(
        Transaction transaction,
        CancellationToken cancellationToken);
    
    // useful for statistics later (monthly spend, store analytics, etc.)
    Task<ErrorOr<GetTransactionsResult<Transaction>>> GetTransactionsByDateRangeAsync(
        Guid userId,
        DateOnly startDate,
        DateOnly endDate,
        CancellationToken cancellationToken);
}