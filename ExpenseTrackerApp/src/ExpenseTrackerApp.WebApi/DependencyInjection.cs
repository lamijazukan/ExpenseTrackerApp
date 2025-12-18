using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ExpenseTrackerApp.Application.MessageHistory;
using ExpenseTrackerApp.Infrastructure.MessageHistory;
using ExpenseTrackerApp.Infrastructure.MessageHistory.Options;
using ExpenseTrackerApp.WebApi.Options;

namespace ExpenseTrackerApp.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.TryAddMessageHistoryOptions(configuration.GetMessageHistoryOptions());

        return services
            .AddMessageHistoryApplication();
    }
    
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddMessageHistoryInfrastructure(configuration);
    }
}