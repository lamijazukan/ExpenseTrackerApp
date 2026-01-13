using ExpenseTrackerApp.Application.Users.Interfaces.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;


namespace ExpenseTrackerApp.Infrastructure.Users;

public static class DependencyInjection
{
    public static  IServiceCollection AddUsersInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.TryAddScoped<IUserRepository, UserRepository>();
        
        return services;
    }
}