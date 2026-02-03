using ExpenseTrackerApp.Application.Transactions.Interfaces.Application;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ExpenseTrackerApp.Application.Transactions;

public static class DependencyInjection
{
    public static IServiceCollection AddTransactionsApplication(this IServiceCollection services)
    {
        services.TryAddScoped<ITransactionService, TransactionService>();
        return services;
    }
    
}