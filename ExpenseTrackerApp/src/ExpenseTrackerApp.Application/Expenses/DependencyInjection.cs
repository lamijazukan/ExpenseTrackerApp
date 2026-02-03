using ExpenseTrackerApp.Application.Budgets.Interfaces.Application;
using ExpenseTrackerApp.Application.Expenses.Interfaces.Application;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ExpenseTrackerApp.Application.Expenses;

public static class DependencyInjection
{
    public static IServiceCollection AddExpensesApplication(this IServiceCollection services)
    {
        services.TryAddScoped<IExpenseService, ExpenseService>();
        return services;
    }
    
}