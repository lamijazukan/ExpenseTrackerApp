using ExpenseTrackerApp.Domain.Enums;

namespace ExpenseTrackerApp.Domain.Entities;

public class UserProfile
{
    public int UserProfileId { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public Language Language { get; set; } = Language.Bs; 
    public Currency Currency { get; set; } = Currency.BAM; 
    
    public string? AvatarUrl { get; set; }
}