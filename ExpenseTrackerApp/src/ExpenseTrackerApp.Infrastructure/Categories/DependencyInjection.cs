using ExpenseTrackerApp.Application.Categories.Interfaces.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ExpenseTrackerApp.Infrastructure.Categories;

public static class DependencyInjection
{
    public static IServiceCollection AddCategoriesInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.TryAddScoped<ICategoryRepository, CategoryRepository>();
        
        return services;
    }
}