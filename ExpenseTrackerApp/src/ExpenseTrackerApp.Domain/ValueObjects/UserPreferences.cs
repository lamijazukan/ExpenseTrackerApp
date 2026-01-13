using ExpenseTrackerApp.Domain.Enums;

namespace ExpenseTrackerApp.Domain.ValueObjects;

public class UserPreferences
{
    public Language Language { get; set; }
    public Currency Currency { get; set; }
    
   
}