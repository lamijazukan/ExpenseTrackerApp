using ExpenseTrackerApp.Domain.Entities;

namespace ExpenseTrackerApp.Application.Authentication.Interfaces.Infrastructure;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}