namespace ExpenseTrackerApp.Application.Users.Interfaces.Infrastructure;

public interface ICurrentUser
{
    Guid UserId { get; }
    bool IsAuthenticated { get; }
}
