using ErrorOr;
using ExpenseTrackerApp.Domain.Entities;
using ExpenseTrackerApp.Domain.Enums;
using ExpenseTrackerApp.Domain.Errors;

namespace ExpenseTrackerApp.Application.UserProfiles;

public class UserProfileValidator
{
    public static ErrorOr<Success> ValidateUserProfile(Language language, Currency currency, string avatarUrl)

    {
        if (!Enum.IsDefined(typeof(Language), language))
        {
            return UserProfileErrors.InvalidLanguage;
        }
        
        if (!Enum.IsDefined(typeof(Language), language))
        {
            return UserProfileErrors.InvalidCurrency;
        }

        if (avatarUrl is not null)
        {


            if (!IsValidUrl(avatarUrl))
            {
                return UserProfileErrors.AvatarUrlInvalid;
            }
        }


        return Result.Success;

    }
    
    private static bool IsValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out _);
    }

}