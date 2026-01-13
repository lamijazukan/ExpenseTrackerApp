namespace ExpenseTrackerApp.Contracts.Users;

public class UpdateUserRequest
{
   
 
    public string? Username { get; set; } 
        
    public string? Password { get; set; } 
        
    public UserPreferencesObject? Preferences { get; set; }
}