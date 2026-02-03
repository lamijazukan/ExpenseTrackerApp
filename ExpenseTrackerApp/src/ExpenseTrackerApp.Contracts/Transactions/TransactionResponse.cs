namespace ExpenseTrackerApp.Contracts.Transactions;

public class TransactionResponse
{
    public int TransactionId { get; set; }
    
    public DateOnly PaidDate { get; set; }
    
    public string Store { get; set; } = string.Empty;
    
    public decimal TotalAmount { get; set; }
 
    public string PaymentMethod { get; set; }
}