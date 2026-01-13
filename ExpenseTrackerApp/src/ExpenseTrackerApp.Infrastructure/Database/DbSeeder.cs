using ExpenseTrackerApp.Domain.Entities;
using ExpenseTrackerApp.Domain.Enums;
using ExpenseTrackerApp.Domain.ValueObjects;

namespace ExpenseTrackerApp.Infrastructure.Database;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        /*USERS*/
        if (!context.Users.Any())
        {
            context.Users.AddRange(
                new User
                {
                    UserId = Guid.Parse("550e8400-e29b-41d4-a716-446655440001"),
                    Username = "john_doe",
                    Email = "john.doe@email.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("StrongPassword123##@"),
                    Preferences = new UserPreferences
                    {
                        Language = Language.Bs,
                        Currency = Currency.BAM
                    }
                },
                new User
                {
                    UserId = Guid.Parse("550e8400-e29b-41d4-a716-446655440002"),
                    Username = "jane_smith",
                    Email = "jane.smith@email.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!=#"),
                    Preferences = new UserPreferences
                    {
                        Language = Language.En,
                        Currency = Currency.USD
                    }
                }
            );
            await context.SaveChangesAsync();
        }
    }
}