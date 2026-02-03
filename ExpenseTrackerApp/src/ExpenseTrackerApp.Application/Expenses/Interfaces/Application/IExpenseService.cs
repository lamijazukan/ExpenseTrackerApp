using ErrorOr;
using ExpenseTrackerApp.Application.Expenses.Data;

namespace ExpenseTrackerApp.Application.Expenses.Interfaces.Application;

public interface IExpenseService
{
    Task<ErrorOr<GetExpensesResult<ExpenseResult>>> GetAllExpensesAsync(CancellationToken cancellationToken);

    Task<ErrorOr<ExpenseResult>> GetExpenseByIdAsync(int expenseId, CancellationToken cancellationToken);

    Task<ErrorOr<ExpenseResult>> CreateExpenseAsync(
        int transactionId,
        int categoryId,
        decimal amount,
        string productName,
        CancellationToken cancellationToken);

    Task<ErrorOr<ExpenseResult>> UpdateExpenseAsync(
        int expenseId,
        int? transactionId,
        int? categoryId,
        decimal? amount,
        string? productName,
        CancellationToken cancellationToken);

    Task<ErrorOr<Success>> DeleteExpenseAsync(int expenseId, CancellationToken cancellationToken);
}