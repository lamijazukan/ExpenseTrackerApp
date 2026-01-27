
using ExpenseTrackerApp.Application.Categories.Interfaces.Application;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ExpenseTrackerApp.Application.Categories;

public static class DependencyInjection
{
    public static IServiceCollection AddCategoriesApplication(this IServiceCollection services)
    {
        services.TryAddScoped<ICategoryService, CategoryService>();
        return services;
    }
}