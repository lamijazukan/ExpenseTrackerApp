using System.Text.Json.Serialization;
using ExpenseTrackerApp.Application;
using ExpenseTrackerApp.Infrastructure.Database;
using ExpenseTrackerApp.WebApi;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Asp.Versioning;
using ExpenseTrackerApp.WebApi.Mappings;

var builder = WebApplication.CreateBuilder(args);
{
    Directory.SetCurrentDirectory(AppContext.BaseDirectory);
    builder.Configuration
        .SetBasePath(AppContext.BaseDirectory);

    // Create the logger
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Filter.ByExcluding(logEvent => 
            logEvent.Properties.TryGetValue("RequestPath", out var property)
            && property.ToString().StartsWith("\"/health"))
        .CreateLogger();
    
    // Add logger to logging pipeline
    builder.Logging
        .ClearProviders()
        .AddSerilog(Log.Logger);
    
    builder.Host.UseSerilog();
    
    //to serialize enums as strings
    builder.Services
        .AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(
                new JsonStringEnumConverter()
            );
        });
    
    builder.Services.AddControllers();
    
    builder.Services.AddRouting(options => 
    {
        options.LowercaseUrls = true;
    });


    // Add API Versioning
    builder.Services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
        options.ApiVersionReader = ApiVersionReader.Combine(
            new UrlSegmentApiVersionReader()
        );
    }).AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

    builder.Services.AddEndpointsApiExplorer();
    
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "Expense Tracker API",
            Version = "v1",
            Description = "A RESTful API for managing expenses, users, and categories in an expense tracking application.",
            Contact = new Microsoft.OpenApi.Models.OpenApiContact
            {
                Name = "API Support",
                Email = "support@expensetracker.com"
            }
        });
        
        // Include XML comments
        var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath))
        {
            c.IncludeXmlComments(xmlPath);
        }
        
        // Include XML comments from referenced projects
        var contractsXml = Path.Combine(AppContext.BaseDirectory, "SampleCkWebApp.Contracts.xml");
        if (File.Exists(contractsXml))
        {
            c.IncludeXmlComments(contractsXml);
        }
        
        // Use full names for better organization
        c.CustomSchemaIds(type => type.FullName);
    });    
    
    builder.Services
        .AddApplication(builder.Configuration)
        .AddInfrastructure(builder.Configuration);
}
/*AUTOMAPPER REGISTRATIONS*/

builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);
builder.Services.AddAutoMapper(typeof(CategoryProfile).Assembly);
builder.Services.AddAutoMapper(typeof(BudgetProfile).Assembly);
builder.Services.AddAutoMapper(typeof(TransactionProfile).Assembly);
builder.Services.AddAutoMapper(typeof(ExpenseProfile).Assembly);

Log.Logger.Information("Application starting");

var app = builder.Build();
{
    
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        // assure DB exists and schema is up-to-date
        db.Database.Migrate();

        await DbSeeder.SeedAsync(db); 
    }
    
    app.UseRouting();
    
    // Save service provider to static class
    ServicePool.Create(app.Services);

    // Add exception handler and request logging
    app.UseExceptionHandler("/errors");
    
    app.UseSerilogRequestLogging(options =>
    {
        options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
        {
            diagnosticContext.ConfigureAuditLogging(httpContext);
        };
    });
    
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Expense Tracker API v1");
        c.RoutePrefix = "swagger"; // Swagger UI available at /swagger
        c.DocumentTitle = "Expense Tracker API Documentation";
        c.DefaultModelsExpandDepth(-1); // Hide schemas by default
        c.DisplayRequestDuration();
        c.EnableDeepLinking();
        c.EnableFilter();
        c.ShowExtensions();
    });
    
    app.MapControllers();
}

// Start the application
app.Start();

Log.Logger.Information("Application started");
foreach (var url in app.Urls)
{
    Log.Logger.Information("Listening on: {url}", url);
}

app.WaitForShutdown();

Log.Logger.Information("Application shutdown gracefully");