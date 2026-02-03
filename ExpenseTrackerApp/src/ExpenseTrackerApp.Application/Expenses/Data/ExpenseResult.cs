namespace ExpenseTrackerApp.Application.Expenses.Data;

public class ExpenseResult
{
    public int ExpenseId { get; set; }

   

    public int CategoryId { get; set; }
    
    

    public decimal Amount { get; set; }

    public DateOnly ExpenseDate { get; set; }

    public string? Description { get; set; }
    public string? PaymentMethod { get; set; }
    
    public DateTime CreatedAt { get; set; }
}