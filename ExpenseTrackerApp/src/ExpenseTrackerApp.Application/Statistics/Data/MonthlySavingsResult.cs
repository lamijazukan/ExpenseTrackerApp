namespace ExpenseTrackerApp.Application.Statistics.Data;


public class MonthlySavingsResult
{
    public int Year { get; init; }
    public int Month { get; init; }

    public decimal Income { get; init; }
    public decimal Expenses { get; init; }
    public decimal Savings { get; init; }

    public bool IsNegative => Savings < 0;
}