namespace ExpenseTrackerApp.Contracts.Budgets;

public class BudgetResponse
{
    public int BudgetId { get; set; }
    public int CategoryId { get; set; }

    public decimal Amount { get; set; }

    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

}