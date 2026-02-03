namespace ExpenseTrackerApp.Application.Expenses.Data;

public class ExpenseResult
{
    public int ExpenseId { get; set; }
    
    public int TransactionId { get; set; }
    
    public int CategoryId { get; set; }
    
    public decimal Amount { get; set; }
    
    public string ProductName { get; set; }
}