namespace ExpenseTrackerApp.Contracts.Expenses;

public class UpdateExpenseRequest
{
    public int? TransactionId { get; set; }
    
    public int? CategoryId { get; set; }
    
    public decimal? Amount { get; set; }
    
    public string? ProductName { get; set; } = string.Empty;
}