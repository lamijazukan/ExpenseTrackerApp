using ErrorOr;
using ExpenseTrackerApp.Application.Authentication.Data;

namespace ExpenseTrackerApp.Application.Authentication.Interfaces.Application;

public interface IAuthService
{

    Task<ErrorOr<RegisterResult>> RegisterAsync(string username, string email, string password, CancellationToken cancellationToken);
    
    Task<ErrorOr<LoginResult>> LoginAsync(string email, string password, CancellationToken cancellationToken);
    
}