
using ExpenseTrackerApp.Application.Budgets;
using ExpenseTrackerApp.Application.Users;
using ExpenseTrackerApp.Infrastructure.Users;
using ExpenseTrackerApp.Application.Categories;
using ExpenseTrackerApp.Application.Expenses;
using ExpenseTrackerApp.Application.Statistics;
using ExpenseTrackerApp.Application.Transactions;
using ExpenseTrackerApp.Infrastructure.Budgets;
using ExpenseTrackerApp.Infrastructure.Categories;

using ExpenseTrackerApp.Infrastructure.Database;
using ExpenseTrackerApp.Infrastructure.DomainEventDispatcher;
using ExpenseTrackerApp.Infrastructure.Expenses;
using ExpenseTrackerApp.Infrastructure.Transactions;


namespace ExpenseTrackerApp.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {


        return services
            .AddUsersApplication()
            .AddCategoriesApplication()
            .AddBudgetsApplication()
            .AddTransactionsApplication()
            .AddExpensesApplication()
            .AddStatisticsApplication();

    }
    
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
      
        return services
            .AddDatabase(configuration)
            .AddUsersInfrastructure(configuration)
            .AddCategoriesInfrastructure(configuration)
            .AddBudgetsInfrastructure(configuration)
            .AddTransactionInfrastructure(configuration)
            .AddExpensesInfrastructure(configuration)
            .AddDomainEventsInfrastructure(configuration);
         
        
    }
}