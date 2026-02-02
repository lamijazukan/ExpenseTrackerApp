namespace ExpenseTrackerApp.Contracts.Budgets;

public class GetBudgetsResponse
{
    public List<BudgetResponse> Budgets { get; set; }
    public int TotalCount { get; set; }
}