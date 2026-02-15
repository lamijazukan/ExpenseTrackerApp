using ErrorOr;

namespace ExpenseTrackerApp.Domain.Errors;

public class UserProfileErrors
{
    public static Error NotFound =>
        Error.NotFound($"{nameof(UserProfileErrors)}.{nameof(NotFound)}", "UserProfile details not found.");
    
    public static Error InvalidLanguage => 
        Error.Validation($"{nameof(UserProfileErrors)}.{nameof(InvalidLanguage)}", "The language is invalid. Valid languages are: en and bs.");
    
    public static Error InvalidCurrency => 
        Error.Validation($"{nameof(UserProfileErrors)}.{nameof(InvalidCurrency)}", "The currency is invalid. Valid currencies are: BAM and USD");
    
    
    public static Error AvatarUrlInvalid =>
        Error.Validation("AvatarUrl must be a valid URL.");
}