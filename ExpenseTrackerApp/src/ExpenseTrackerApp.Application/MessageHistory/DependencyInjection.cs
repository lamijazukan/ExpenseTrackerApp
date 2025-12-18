using ExpenseTrackerApp.Application.MessageHistory.Interfaces.Application;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ExpenseTrackerApp.Application.MessageHistory;

public static class DependencyInjection
{
    public static IServiceCollection AddMessageHistoryApplication(this IServiceCollection services)
    {
        services.TryAddSingleton<IMessageHistoryService, MessageHistoryService>();
        return services;
    }
}