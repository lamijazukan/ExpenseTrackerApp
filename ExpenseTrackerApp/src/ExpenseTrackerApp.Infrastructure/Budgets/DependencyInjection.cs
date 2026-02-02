using ExpenseTrackerApp.Application.Budgets.Interfaces.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ExpenseTrackerApp.Infrastructure.Budgets;

public static class DependencyInjection
{
    public static IServiceCollection AddBudgetsInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.TryAddScoped<IBudgetRepository, BudgetRepository>();
        
        return services;
    }
}