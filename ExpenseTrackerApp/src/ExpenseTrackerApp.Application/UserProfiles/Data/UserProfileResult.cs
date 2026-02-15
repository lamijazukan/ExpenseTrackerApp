using ExpenseTrackerApp.Domain.Enums;

namespace ExpenseTrackerApp.Application.UserProfiles.Data;

public class UserProfileResult
{
    
    public string Username { get; set; }
    public string Email { get; set; }
    public Language Language { get; set; } = Language.Bs; 
    
    public Currency Currency { get; set; } = Currency.BAM; 
    
    public string? AvatarUrl { get; set; }
}