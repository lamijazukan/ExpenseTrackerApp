using ExpenseTrackerApp.Domain.Entities;
using ErrorOr;

namespace ExpenseTrackerApp.Application.UserProfiles.Interfaces.Infrastructure;

public interface IUserProfileRepository
{
    Task<ErrorOr<UserProfile>> GetProfileByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    
    Task<ErrorOr<UserProfile>> CreateProfileAsync(UserProfile profile, CancellationToken cancellationToken);
    Task<ErrorOr<UserProfile>> UpdateProfileAsync(UserProfile profile, CancellationToken cancellationToken);
}