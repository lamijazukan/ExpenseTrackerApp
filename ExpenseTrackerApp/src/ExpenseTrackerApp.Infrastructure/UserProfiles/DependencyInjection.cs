using ExpenseTrackerApp.Application.UserProfiles.Interfaces.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;


namespace ExpenseTrackerApp.Infrastructure.UserProfiles;

public static class DependencyInjection
{
    public static  IServiceCollection AddUserProfilesInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.TryAddScoped<IUserProfileRepository, UserProfileRepository>();
        
        return services;
    }
}