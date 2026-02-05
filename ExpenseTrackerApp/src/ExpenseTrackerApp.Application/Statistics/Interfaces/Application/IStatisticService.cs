using ErrorOr;
using ExpenseTrackerApp.Application.Statistics.Data;

namespace ExpenseTrackerApp.Application.Statistics.Interfaces.Application;

public interface IStatisticService
{
    Task<ErrorOr<BudgetStatusResult>> GetBudgetStatusAsync(
        int budgetId,
        CancellationToken cancellationToken);

    Task<ErrorOr<TotalExpenseByCategoryResult>> GetTotalExpenseByCategoryAsync(
        int categoryId,
        DateOnly from,
        DateOnly to,
        CancellationToken cancellationToken);

    Task<ErrorOr<MonthlySavingsResult>> GetMonthlySavingsAsync(
        int year,
        int month,
        CancellationToken cancellationToken);
    
    
}