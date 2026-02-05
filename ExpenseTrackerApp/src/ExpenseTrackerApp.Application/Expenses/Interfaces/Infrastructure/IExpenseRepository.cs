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
    
    //for statistics purposes
    
    Task<ErrorOr<decimal>> GetTotalExpensesForBudgetPeriodAsync(
        Guid userId,
        int categoryId,
        DateOnly startDate,
        DateOnly endDate,
        CancellationToken cancellationToken);

    Task<ErrorOr<decimal>> GetTotalExpensesForMonthAsync(
        Guid userId,
        int year,
        int month,
        CancellationToken cancellationToken);
}