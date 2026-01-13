using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;


namespace ExpenseTrackerApp.Infrastructure.Database;

public sealed class DatabaseOptions
{
    public const string SectionName = "Database";

    public string? ConnectionString { get; init; }

    
    
}
