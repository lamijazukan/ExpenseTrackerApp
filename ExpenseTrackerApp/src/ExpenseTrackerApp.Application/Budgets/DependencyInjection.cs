using ExpenseTrackerApp.Application.Budgets.Interfaces.Application;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ExpenseTrackerApp.Application.Budgets;

public static class DependencyInjection
{
    public static IServiceCollection AddBudgetsApplication(this IServiceCollection services)
    {
        services.TryAddScoped<IBudgetService, BudgetService>();
        return services;
    }
    
}