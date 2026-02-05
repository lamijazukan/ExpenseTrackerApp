using ExpenseTrackerApp.Application.Budgets.Interfaces.Infrastructure;
using ExpenseTrackerApp.Domain;
using ExpenseTrackerApp.Infrastructure.Budgets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ExpenseTrackerApp.Infrastructure.DomainEventDispatcher;

public static class DependencyInjection
{
    public static IServiceCollection AddDomainEventsInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.TryAddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        
        return services;
    }
}