using ExpenseTrackerApp.Application.Authentication.Interfaces.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;


namespace ExpenseTrackerApp.Infrastructure.Authentication;

public static class DependencyInjection
{
    public static  IServiceCollection AddJwtGeneratorInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.TryAddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        
        return services;
    }
}