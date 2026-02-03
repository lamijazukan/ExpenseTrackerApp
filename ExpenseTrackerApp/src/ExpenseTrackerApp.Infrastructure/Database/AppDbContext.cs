using ExpenseTrackerApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace ExpenseTrackerApp.Infrastructure.Database;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }
    public DbSet<User> Users { get; set; } 
    public DbSet<Category> Categories { get; set; }
    
    public DbSet<Budget> Budgets { get; set; }
    
    public DbSet<Expense> Expenses { get; set; }
    
    public DbSet<Transaction> Transactions { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all IEntityTypeConfiguration<T> automatically
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}