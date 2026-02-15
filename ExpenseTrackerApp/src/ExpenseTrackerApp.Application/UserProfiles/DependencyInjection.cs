using ExpenseTrackerApp.Application.UserProfiles.Interfaces.Application;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ExpenseTrackerApp.Application.UserProfiles;

public static class DependencyInjection
{
    public static IServiceCollection AddUserProfilesApplication(this IServiceCollection services)
    {
        services.TryAddScoped<IUserProfileService, UserProfileService>();
        return services;
    }
    
}