using ErrorOr;
using ExpenseTrackerApp.Application.Users.Data;


namespace ExpenseTrackerApp.Application.Users.Interfaces.Application;

public interface IUserService
{
    Task<ErrorOr<GetUsersResult<UserResult>>> GetUsersAsync(CancellationToken cancellationToken);
    Task<ErrorOr<UserResult>> GetUserByIdAsync(Guid userId,  CancellationToken cancellationToken);
    Task<ErrorOr<UserResult>> UpdateUserAsync(Guid userId, string username, string password, CancellationToken cancellationToken);
}