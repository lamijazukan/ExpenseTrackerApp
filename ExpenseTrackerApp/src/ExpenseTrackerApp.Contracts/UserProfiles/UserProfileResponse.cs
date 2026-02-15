namespace ExpenseTrackerApp.Contracts.UserProfiles;

public class UserProfileResponse
{
    public string Username { get; set; } 
    public string Email { get; set; }
    public Language Language { get; set; }
    public Currency Currency { get; set; }
    public string? AvatarUrl { get; set; }
}