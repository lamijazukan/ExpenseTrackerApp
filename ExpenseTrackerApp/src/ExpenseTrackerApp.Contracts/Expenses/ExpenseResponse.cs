namespace ExpenseTrackerApp.Contracts.Expenses;

public class ExpenseResponse
{
    public int ExpenseId { get; set; }
    
    public int TransactionId { get; set; }
    
    public int CategoryId { get; set; }
    
    public decimal Amount { get; set; }
    
    public string ProductName { get; set; }
}