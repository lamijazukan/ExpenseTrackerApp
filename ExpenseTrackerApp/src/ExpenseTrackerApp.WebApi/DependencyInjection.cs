
using ExpenseTrackerApp.Application.Budgets;
using ExpenseTrackerApp.Application.Users;
using ExpenseTrackerApp.Infrastructure.Users;
using ExpenseTrackerApp.Application.Categories;
using ExpenseTrackerApp.Infrastructure.Budgets;
using ExpenseTrackerApp.Infrastructure.Categories;

using ExpenseTrackerApp.Infrastructure.Database;


namespace ExpenseTrackerApp.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {


        return services
            .AddUsersApplication()
            .AddCategoriesApplication()
            .AddBudgetsApplication();

    }
    
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
      
        return services
            .AddDatabase(configuration)
            .AddUsersInfrastructure(configuration)
            .AddCategoriesInfrastructure(configuration)
            .AddBudgetsInfrastructure(configuration);
         
        
    }
}