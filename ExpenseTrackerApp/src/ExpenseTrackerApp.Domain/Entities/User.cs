
using ExpenseTrackerApp.Domain.ValueObjects;

namespace ExpenseTrackerApp.Domain.Entities;

public class User
{
    public Guid UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    public UserPreferences Preferences { get; set; }
    
    // Navigation properties
    /*public ICollection<Category> Categories { get; set; } = new List<Category>();
    public ICollection<Budget> Budgets { get; set; } = new List<Budget>();
    public ICollection<Receipt> Receipts { get; set; } = new List<Receipt>();
    public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();*/
}