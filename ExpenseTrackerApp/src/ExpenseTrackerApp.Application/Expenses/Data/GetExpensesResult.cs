using ExpenseTrackerApp.Domain.Entities;

namespace ExpenseTrackerApp.Application.Expenses.Data;

public class GetExpensesResult<T>
{
    public List<T> Expenses { get; set; }
    
    public int TotalCount { get; set; }
}