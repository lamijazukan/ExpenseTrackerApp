using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ExpenseTrackerApp.Application.MessageHistory.Interfaces.Infrastructure;
using ExpenseTrackerApp.Infrastructure.MessageHistory.Options;

namespace ExpenseTrackerApp.Infrastructure.MessageHistory;

public static class DependencyInjection
{
    public static IServiceCollection AddMessageHistoryInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.TryAddMessageHistoryOptions(configuration.GetMessageHistoryOptions());
        
        services.TryAddSingleton<IMessageHistoryRepository, MessageHistoryRepository>();
        
        return services;
    }
}