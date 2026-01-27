using ErrorOr;
using ExpenseTrackerApp.Application.Users.Data;
using ExpenseTrackerApp.Domain.Entities;
using ExpenseTrackerApp.Domain.ValueObjects;

namespace ExpenseTrackerApp.Application.Users.Interfaces.Application;

public interface IUserService
{
    Task<ErrorOr<GetUsersResult>> GetUsersAsync(CancellationToken cancellationToken);
    Task<ErrorOr<UserResult>> GetUserByIdAsync(Guid userId,  CancellationToken cancellationToken);
    
    Task<ErrorOr<UserResult>> CreateUserAsync(string username, string email, string password, UserPreferences preferences, CancellationToken cancellationToken);
    
    Task<ErrorOr<UserResult>> UpdateUserAsync(Guid userId, string username, string password, UserPreferences preferences, CancellationToken cancellationToken);
}