using ExpenseTrackerApp.Application.Authentication.Interfaces.Application;
using ExpenseTrackerApp.Application.Users.Interfaces.Application;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ExpenseTrackerApp.Application.Authentication;

public static class DependencyInjection
{
    public static IServiceCollection AddAuthenticationApplication(this IServiceCollection services)
    {
        services.TryAddScoped<IAuthService, AuthService>();
        return services;
    }
}