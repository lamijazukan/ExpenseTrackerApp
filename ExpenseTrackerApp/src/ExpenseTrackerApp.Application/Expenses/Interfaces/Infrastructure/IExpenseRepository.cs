using ErrorOr;
using ExpenseTrackerApp.Application.Expenses.Data;
using ExpenseTrackerApp.Domain.Entities;

namespace ExpenseTrackerApp.Application.Expenses.Interfaces.Infrastructure;

public interface IExpenseRepository
{
    Task<ErrorOr<GetExpensesResult<Expense>>> GetExpensesByUserIdAsync(
        Guid userId, CancellationToken cancellationToken);

    Task<ErrorOr<Expense>> GetExpenseByIdAsync(
        Guid userId, int expenseId, CancellationToken cancellationToken);

    Task<ErrorOr<Expense>> CreateExpenseAsync(
        Expense expense, CancellationToken cancellationToken);

    Task<ErrorOr<Expense>> UpdateExpenseAsync(
        Expense expense, CancellationToken cancellationToken);

    Task<ErrorOr<Success>> DeleteExpenseAsync(
        Expense expense, CancellationToken cancellationToken);
}