namespace ExpenseTrackerApp.Contracts.Transactions;

public class UpdateTransactionRequest
{
    public DateOnly? PaidDate { get; set; }
    
    public string? Store { get; set; }
    
    public decimal? TotalAmount { get; set; }
 
    public string? PaymentMethod { get; set; }
}