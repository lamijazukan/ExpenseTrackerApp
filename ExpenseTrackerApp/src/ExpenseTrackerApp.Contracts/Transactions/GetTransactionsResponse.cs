using System.Transactions;

namespace ExpenseTrackerApp.Contracts.Transactions;

public class GetTransactionsResponse
{
    public List<TransactionResponse> Transactions { get; set; }
    
    public int TotalCount { get; set; }
}