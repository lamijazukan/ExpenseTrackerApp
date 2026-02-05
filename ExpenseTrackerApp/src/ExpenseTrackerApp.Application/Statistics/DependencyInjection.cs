using ExpenseTrackerApp.Application.Statistics.Interfaces.Application;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ExpenseTrackerApp.Application.Statistics;

public static class DependencyInjection
{
    public static IServiceCollection AddStatisticsApplication(this IServiceCollection services)
    {
        services.TryAddScoped<IStatisticService, StatisticsService>();
        return services;
    }
    
}