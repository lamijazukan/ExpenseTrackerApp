using ErrorOr;
using ExpenseTrackerApp.Application.Users.Data;
using ExpenseTrackerApp.Domain.Entities;

namespace ExpenseTrackerApp.Application.Users.Interfaces.Infrastructure;

public interface IUserRepository
{
    Task<ErrorOr<GetUsersResult<User>>> GetUsersAsync(CancellationToken cancellationToken);
    
    Task<ErrorOr<User>> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
    
    Task<ErrorOr<User>> GetUserByEmailAsync(string email, CancellationToken cancellationToken);

    Task<ErrorOr<User>> CreateUserAsync(User user, CancellationToken cancellationToken);
    
    Task<ErrorOr<User>> UpdateUserAsync(User user, CancellationToken cancellationToken);
    
    Task<ErrorOr<bool>> EmailExistsAsync(string email, CancellationToken cancellationToken);

}