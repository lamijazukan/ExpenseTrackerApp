namespace ExpenseTrackerApp.Contracts.Statistics;

public record GetBudgetStatusResponse
{
    public decimal TotalAmount { get; init; }
    public decimal SpentAmount { get; init; }
    public decimal RemainingAmount { get; init; }
    public bool IsExceeded { get; init; }
    public bool IsNearLimit { get; init; }
}