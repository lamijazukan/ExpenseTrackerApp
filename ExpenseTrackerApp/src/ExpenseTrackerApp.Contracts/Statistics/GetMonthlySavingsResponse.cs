namespace ExpenseTrackerApp.Contracts.Statistics;

public record GetMonthlySavingsResponse
{
    public int Year { get; init; }
    public int Month { get; init; }
    public decimal BudgetBalance { get; init; }
    public decimal Expenses { get; init; }
    public decimal Savings { get; init; }
}