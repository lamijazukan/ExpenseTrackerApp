namespace ExpenseTrackerApp.Contracts.Users;

public class UserResponse
{
    public Guid UserId { get; set; }
 
    public string Username { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
    
    public UserPreferencesObject Preferences { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}