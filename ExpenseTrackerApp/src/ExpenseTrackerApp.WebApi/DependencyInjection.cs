
using ExpenseTrackerApp.Application.Users;
using ExpenseTrackerApp.Infrastructure.Users;
using ExpenseTrackerApp.Infrastructure.Database;


namespace ExpenseTrackerApp.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {


        return services
            .AddUsersApplication();

    }
    
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
      
        return services
            .AddDatabase(configuration)
            .AddUsersInfrastructure(configuration);
         
        
    }
}