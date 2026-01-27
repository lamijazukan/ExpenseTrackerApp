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
        //services.AddHttpContextAccessor();
        //services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddScoped<ICurrentUser>(_ =>
            new FakeCurrentUser(Guid.Parse("f5a73417-757d-4a91-a665-3832f4698627")));
        

        
        return services;
    }
}