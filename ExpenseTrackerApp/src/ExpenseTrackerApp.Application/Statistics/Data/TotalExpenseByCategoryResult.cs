namespace ExpenseTrackerApp.Application.Statistics.Data;


public class TotalExpenseByCategoryResult
{
    public int CategoryId { get; init; }
    public DateOnly From { get; init; }
    public DateOnly To { get; init; }

    public decimal TotalAmount { get; init; }
}