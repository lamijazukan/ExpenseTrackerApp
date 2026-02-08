using System.Security.Claims;
using ExpenseTrackerApp.Application.Users.Interfaces.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace ExpenseTrackerApp.Infrastructure.Users;

public class CurrentUser : ICurrentUser
{
    public Guid UserId { get; }
    public bool IsAuthenticated { get; }

    public CurrentUser(IHttpContextAccessor accessor)
    {
        var user = accessor.HttpContext?.User;

        IsAuthenticated = user?.Identity?.IsAuthenticated ?? false;

        if (!IsAuthenticated)
        {
            UserId = Guid.Empty;
            return;
        }

        var userIdClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdClaim, out var parsedUserId))
        {
            UserId = Guid.Empty;
            IsAuthenticated = false;
            return;
        }

        UserId = parsedUserId;
    }
}