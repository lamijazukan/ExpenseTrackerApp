namespace ExpenseTrackerApp.Contracts.Budgets;

public class UpdateBudgetRequest
{
    
    public int? CategoryId { get; set; }

    public decimal? Amount { get; set; }

    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    
}