namespace ExpenseTrackerApp.Contracts.UserProfiles;

public class UpdateUserProfileRequest
{
    public Language Language { get; set; } = Language.Bs;
    public Currency Currency { get; set; } = Currency.BAM;
    public string? AvatarUrl { get; set; }
}