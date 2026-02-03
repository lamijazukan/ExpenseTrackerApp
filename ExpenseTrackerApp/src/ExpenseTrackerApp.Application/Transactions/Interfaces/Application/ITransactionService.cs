using ErrorOr;

using ExpenseTrackerApp.Application.Transactions.Data;


namespace ExpenseTrackerApp.Application.Transactions.Interfaces.Application;

public interface ITransactionService
{
    Task<ErrorOr<GetTransactionsResult<TransactionResult>>> GetAllTransactionsAsync(
        CancellationToken cancellationToken);

    Task<ErrorOr<TransactionResult>> GetTransactionByIdAsync(
        int transactionId,
        CancellationToken cancellationToken);

    Task<ErrorOr<TransactionResult>> CreateTransactionAsync(
        DateOnly paidDate,
        string store,
        decimal totalAmount,
        string paymentMethod,
        CancellationToken cancellationToken);

    Task<ErrorOr<TransactionResult>> UpdateTransactionAsync(
        int transactionId,
        DateOnly? paidDate,
        string? store,
        decimal? totalAmount,
        string? paymentMethod,
        CancellationToken cancellationToken);

    Task<ErrorOr<Success>> DeleteTransactionAsync(
        int transactionId,
        CancellationToken cancellationToken);
}