using ErrorOr;
using ExpenseTrackerApp.Domain.Enums;
using ExpenseTrackerApp.Domain.Errors;
using ExpenseTrackerApp.Domain.ValueObjects;

namespace ExpenseTrackerApp.Application.Users;

public class UserValidator
{
    public static ErrorOr<Success> ValidateCreateUserRequest(string name, string email, string password)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 100)
        {
            return UserErrors.InvalidUsername;
        }

        if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
        {
            return UserErrors.InvalidEmail;
        }

        if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
        {
            return UserErrors.InvalidPassword;
        }

       
        return Result.Success; 
    }

    public static ErrorOr<Success> ValidateUpdateUserRequest(
        string? username,
        string? password,
        UserPreferences? preferences)
    {
        if (username is not null)
        {
            if (string.IsNullOrWhiteSpace(username) || username.Length > 100)
            {
                return UserErrors.InvalidUsername;
            }
        }

        if (password is not null)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
            {
                return UserErrors.InvalidPassword;
            }
        }

        if (preferences is not null)
        {
            if ((preferences.Language != Language.Bs && preferences.Language != Language.En) ||
                (preferences.Currency != Currency.BAM && preferences.Currency != Currency.USD))
            {
                return UserErrors.InvalidPreferences;
            }
        }

        return Result.Success;
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}