using ErrorOr;
using ExpenseTrackerApp.Application.Budgets.Data;
using ExpenseTrackerApp.Domain.Entities;

namespace ExpenseTrackerApp.Application.Budgets.Interfaces.Infrastructure;

public interface IBudgetRepository
{
    Task<ErrorOr<GetBudgetsResult<Budget>>> GetBudgetsByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    
    Task<ErrorOr<Budget>> GetBudgetByIdAsync(Guid userId, int budgetId, CancellationToken cancellationToken);
    
    //for statistics later
    
    Task<ErrorOr<Budget>> GetBudgetByCategoryIdAsync(Guid userId, int categoryId, CancellationToken cancellationToken);
    
    Task<ErrorOr<Budget>> CreateBudgetAsync(Budget budget, CancellationToken cancellationToken);
    
    Task<ErrorOr<Budget>> UpdateBudgetAsync(Budget budget, CancellationToken cancellationToken);
    
    Task<ErrorOr<Success>> DeleteBudgetAsync(Budget budget, CancellationToken cancellationToken);
    
}