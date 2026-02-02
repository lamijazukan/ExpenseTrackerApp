using ExpenseTrackerApp.Domain.Enums;

namespace ExpenseTrackerApp.Domain.Entities;

public class Budget
{
    public int BudgetId { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    
}
