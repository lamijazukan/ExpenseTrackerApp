namespace ExpenseTrackerApp.Domain.Entities;

public class Transaction
{
    public int TransactionId { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    
    public DateOnly PaidDate { get; set; }
    
    public string Store { get; set; } = string.Empty;
    
    public decimal TotalAmount { get; set; }
 
    public string PaymentMethod { get; set; }
    
    public DateTime CreatedAt { get; set; }
}
