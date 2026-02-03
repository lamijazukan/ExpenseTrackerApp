namespace ExpenseTrackerApp.Application.Transactions.Data;

public class GetTransactionsResult<T>
{
    public List<T> Transactions { get; set; } = new ();
    public int TotalCount { get; set; }
}