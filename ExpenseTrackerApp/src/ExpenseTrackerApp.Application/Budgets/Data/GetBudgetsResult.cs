using ExpenseTrackerApp.Domain.Entities;

namespace ExpenseTrackerApp.Application.Budgets.Data;

public class GetBudgetsResult<T>
{
    public List<T> Budgets { get; set; } = new();
    public int TotalCount { get; set; }
}