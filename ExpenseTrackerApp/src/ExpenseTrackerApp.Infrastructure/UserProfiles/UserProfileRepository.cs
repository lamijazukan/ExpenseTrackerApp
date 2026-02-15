using ExpenseTrackerApp.Domain.Entities;
using ExpenseTrackerApp.Infrastructure.Database;
using ErrorOr;
using ExpenseTrackerApp.Application.UserProfiles.Interfaces.Infrastructure;
using ExpenseTrackerApp.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerApp.Infrastructure.UserProfiles;


public class UserProfileRepository : IUserProfileRepository
{
    private readonly AppDbContext _context;

    public UserProfileRepository(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<ErrorOr<UserProfile>> GetProfileByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var profile = await _context.UserProfiles
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.UserId == userId, cancellationToken);

        return profile is null ? UserProfileErrors.NotFound : profile;
    }
    
    public async Task<ErrorOr<UserProfile>> CreateProfileAsync(UserProfile profile, CancellationToken cancellationToken)
    {
        _context.UserProfiles.Add(profile);
        await _context.SaveChangesAsync(cancellationToken);
        return profile;
    }


    public async Task<ErrorOr<UserProfile>> UpdateProfileAsync(UserProfile profile, CancellationToken cancellationToken)
    {
        _context.UserProfiles.Update(profile);
        await _context.SaveChangesAsync(cancellationToken);
        return profile;
    }
}