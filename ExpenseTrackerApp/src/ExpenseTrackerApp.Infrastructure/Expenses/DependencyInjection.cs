using ExpenseTrackerApp.Application.Expenses.Interfaces.Infrastructure;
using ExpenseTrackerApp.Application.Transactions.Interfaces.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ExpenseTrackerApp.Infrastructure.Expenses;

public static class DependencyInjection
{
    public static IServiceCollection AddExpensesInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.TryAddScoped<IExpenseRepository, ExpenseRepository>();
        return services;
    }
    
}