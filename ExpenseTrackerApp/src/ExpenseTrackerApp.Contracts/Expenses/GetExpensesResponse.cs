namespace ExpenseTrackerApp.Contracts.Expenses;

public class GetExpensesResponse
{
    public List<ExpenseResponse> Expenses { get; set; }
    
    public int TotalCount { get; set; }
}