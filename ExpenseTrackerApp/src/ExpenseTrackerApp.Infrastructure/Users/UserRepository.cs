using ErrorOr;
using Microsoft.EntityFrameworkCore;
using ExpenseTrackerApp.Domain.Entities;
using ExpenseTrackerApp.Domain.Errors;
using ExpenseTrackerApp.Application.Users.Interfaces.Infrastructure;
using ExpenseTrackerApp.Infrastructure.Database;

using Npgsql;

namespace ExpenseTrackerApp.Infrastructure.Users;

public  class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<ErrorOr<(List<User> Users, int TotalCount)>> GetUsersAsync(CancellationToken cancellationToken)
    {
        try
        {
            var query = _context.Users
                .AsNoTracking()
                .OrderBy(u => u.CreatedAt);

            var totalCount = await query.CountAsync(cancellationToken);
            var users = await query.ToListAsync(cancellationToken);

            return (users, totalCount);
        }
        catch (Exception ex)
        {
            return Error.Failure(
                "Database.Error",
                $"Failed to retrieve users: {ex.Message}");
        }
    }

    public async Task<ErrorOr<User>> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == userId, cancellationToken);

            return user is null
                ? UserErrors.NotFound
                : user;
        }
        catch (Exception ex)
        {
            return Error.Failure(
                "Database.Error",
                $"Failed to retrieve user: {ex.Message}");
        }
    }

    public async Task<ErrorOr<User>> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

            return user is null
                ? UserErrors.NotFound
                : user;
        }
        catch (Exception ex)
        {
            return Error.Failure(
                "Database.Error",
                $"Failed to retrieve user by email: {ex.Message}");
        }
    }

    public async Task<ErrorOr<User>> CreateUserAsync(User user, CancellationToken cancellationToken)
    {
        try
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return user;
        }
        catch (DbUpdateException ex)
        {
            if (IsUniqueConstraintViolation(ex))
            {
                return UserErrors.DuplicateEmail;
            }

            return Error.Failure(
                "Database.Error",
                $"Failed to create user: {ex.Message}");
        }
    }

    public async Task<ErrorOr<User>> UpdateUserAsync(User user, CancellationToken cancellationToken)
    {
        try
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
            
            return user;

        }
        catch (DbUpdateException ex )
        {
           return Error.Failure(
               "Database.Error",
               $"Failed to update user: {ex.Message}");
        }
    }

    private static bool IsUniqueConstraintViolation(DbUpdateException ex)
    {
        return ex.InnerException is PostgresException pg
            && pg.SqlState == PostgresErrorCodes.UniqueViolation;
    }
}
