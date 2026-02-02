using ErrorOr;
using ExpenseTrackerApp.Application.Budgets.Data;
using ExpenseTrackerApp.Domain.Entities;

namespace ExpenseTrackerApp.Application.Budgets.Interfaces.Application;

public interface IBudgetService
{
    Task<ErrorOr<GetBudgetsResult<BudgetResult>>> GetAllBudgetsAsync(CancellationToken cancellationToken);
    
    Task<ErrorOr<BudgetResult>> GetBudgetByIdAsync (int budgetId, CancellationToken cancellationToken);
    
   
    Task<ErrorOr<BudgetResult>> CreateBudgetAsync (int categoryId, decimal amount, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken);
    
    Task<ErrorOr<BudgetResult>> UpdateBudgetAsync (int budgetId, decimal? amount, DateOnly? startDate, DateOnly? endDate, CancellationToken cancellationToken);

   
    
    Task<ErrorOr<Success>> DeleteBudgetAsync(int budgetId, CancellationToken cancellationToken);
}