using ErrorOr;

namespace ExpenseTrackerApp.Domain.Errors;

public class UserErrors
{
    public static Error NotFound =>
        Error.NotFound($"{nameof(UserErrors)}.{nameof(NotFound)}", "User not found.");
    
    public static Error DuplicateEmail =>
        Error.Conflict($"{nameof(UserErrors)}.{nameof(DuplicateEmail)}", "A user with this email already exists.");
    
    public static Error InvalidEmail =>
        Error.Validation($"{nameof(UserErrors)}.{nameof(InvalidEmail)}", "The provided email is invalid.");
    
    public static Error InvalidUsername =>
        Error.Validation($"{nameof(UserErrors)}.{nameof(InvalidUsername)}", "Username must be between 4 and 100 characters.");
    
    public static Error InvalidPassword =>
        Error.Validation($"{nameof(UserErrors)}.{nameof(InvalidPassword)}", "Password is required and must be at least 8 characters long.");
    
    public static Error InvalidPreferences => 
        Error.Validation($"{nameof(UserErrors)}.{nameof(InvalidPreferences)}", "The preferences are invalid. Valid languages are: en and bs. Valid currencies are: BAM and USD");
}