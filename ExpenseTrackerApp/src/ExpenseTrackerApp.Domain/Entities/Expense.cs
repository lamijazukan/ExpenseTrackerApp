namespace ExpenseTrackerApp.Domain.Entities;

public class Expense
{
    public int ExpenseId { get; set; }
    
    public int TransactionId { get; set; }
    public Transaction Transaction { get; set; } = null!;
    
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    
    public decimal Amount { get; set; }
    
    public string ProductName { get; set; } = string.Empty;
    
}
