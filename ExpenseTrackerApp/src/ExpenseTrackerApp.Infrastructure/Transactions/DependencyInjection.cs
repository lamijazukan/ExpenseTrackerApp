using ExpenseTrackerApp.Application.Transactions.Interfaces.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ExpenseTrackerApp.Infrastructure.Transactions;

public static class DependencyInjection
{
    public static IServiceCollection AddTransactionInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.TryAddScoped<ITransactionRepository, TransactionRepository>();
        return services;
    }
    
}