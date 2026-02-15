using ExpenseTrackerApp.Domain.Entities;
using ErrorOr;
using ExpenseTrackerApp.Application.UserProfiles.Data;
using ExpenseTrackerApp.Domain.Enums;

namespace ExpenseTrackerApp.Application.UserProfiles.Interfaces.Application;

public interface IUserProfileService
{
    Task<ErrorOr<UserProfileResult>> GetProfileAsync(CancellationToken cancellationToken);
    Task<ErrorOr<UserProfileResult>> UpdateProfileAsync(Language language, Currency currency, string? avatarUrl, CancellationToken cancellationToken);
}