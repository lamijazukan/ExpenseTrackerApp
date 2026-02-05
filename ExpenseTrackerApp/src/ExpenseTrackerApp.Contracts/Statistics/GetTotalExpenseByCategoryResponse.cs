namespace ExpenseTrackerApp.Contracts.Statistics;

public record GetTotalExpenseByCategoryResponse
{
    public int CategoryId { get; init; }
    public DateOnly From { get; init; }
    public DateOnly To { get; init; }
    public decimal TotalAmount { get; init; }
}