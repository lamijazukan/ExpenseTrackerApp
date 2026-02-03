namespace ExpenseTrackerApp.Contracts.Expenses;

public class CreateExpenseRequest
{
    public int TransactionId { get; set; }
    
    public int CategoryId { get; set; }
    
    public decimal Amount { get; set; }
    
    public string ProductName { get; set; } 
}