namespace ExpenseTrackerApp.Contracts.Transactions;

public class UpdateTransactionRequest
{
    public DateOnly? PaidDate { get; set; }
    
    public string? Store { get; set; } = string.Empty;
    
    public decimal? TotalAmount { get; set; }
 
    public string? PaymentMethod { get; set; }
}