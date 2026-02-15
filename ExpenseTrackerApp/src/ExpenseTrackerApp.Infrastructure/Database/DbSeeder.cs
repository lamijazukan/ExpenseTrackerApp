using ExpenseTrackerApp.Domain.Entities;
using ExpenseTrackerApp.Domain.Enums;

namespace ExpenseTrackerApp.Infrastructure.Database;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        /* ==================== USERS ==================== */
        if (!context.Users.Any())
        {
            var demoUser = new User
            {
                UserId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Username = "demo",
                Email = "demo@demo.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Demo123!"),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var testUser = new User
            {
                UserId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Username = "testuser",
                Email = "test@test.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Test123!"),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            context.Users.AddRange(demoUser, testUser);
            await context.SaveChangesAsync();
            
            
            // USER PROFILES

            if (!context.UserProfiles.Any())
            {
                context.UserProfiles.AddRange(
                    new UserProfile
                    {
                        UserId = demoUser.UserId,
                        Language = Language.Bs,
                        Currency = Currency.BAM
                    },
                    new UserProfile
                    {
                        UserId = testUser.UserId,
                        Language = Language.En,
                        Currency = Currency.USD
                    }

                );
            }

            await context.SaveChangesAsync();
        }

        /* ==================== CATEGORIES ==================== */
        if (!context.Categories.Any())
        {
            var demoUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

            // Parent category
            var food = new Category { Name = "Food", UserId = demoUserId };
            var transport = new Category { Name = "Transport", UserId = demoUserId };

            context.Categories.AddRange(food, transport);
            await context.SaveChangesAsync(); // Save to get CategoryIds

            // Subcategories (children)
            var groceries = new Category
            {
                Name = "Groceries",
                UserId = demoUserId,
                ParentCategoryId = food.CategoryId
            };

            var restaurants = new Category
            {
                Name = "Restaurants",
                UserId = demoUserId,
                ParentCategoryId = food.CategoryId
            };

            context.Categories.AddRange(groceries, restaurants);
            await context.SaveChangesAsync();
        }

        /* ==================== BUDGETS ==================== */
        if (!context.Budgets.Any())
        {
            var demoUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var categories = context.Categories.Where(c => c.UserId == demoUserId).ToList();

            context.Budgets.AddRange(
                new Budget
                {
                    UserId = demoUserId,
                    CategoryId = categories.First(c => c.Name == "Food").CategoryId,
                    Amount = 400,
                    StartDate = new DateOnly(2026, 2, 1),
                    EndDate = new DateOnly(2026, 2, 28)
                },
                new Budget
                {
                    UserId = demoUserId,
                    CategoryId = categories.First(c => c.Name == "Transport").CategoryId,
                    Amount = 150,
                    StartDate = new DateOnly(2026, 2, 1),
                    EndDate = new DateOnly(2026, 2, 28)
                }
            );

            await context.SaveChangesAsync();
        }

        /* ==================== TRANSACTIONS ==================== */
        if (!context.Transactions.Any())
        {
            var demoUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

            context.Transactions.AddRange(
                new Transaction
                {
                    UserId = demoUserId,
                    PaidDate = new DateOnly(2026, 2, 3),
                    Store = "Supermarket",
                    TotalAmount = 80,
                    PaymentMethod = "Credit Card",
                    CreatedAt = DateTime.UtcNow
                },
                new Transaction
                {
                    UserId = demoUserId,
                    PaidDate = new DateOnly(2026, 2, 5),
                    Store = "Gas Station",
                    TotalAmount = 40,
                    PaymentMethod = "Cash",
                    CreatedAt = DateTime.UtcNow
                }
            );

            await context.SaveChangesAsync();
        }

        /* ==================== EXPENSES ==================== */
        if (!context.Expenses.Any())
        {
            var demoUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var transactions = context.Transactions.Where(t => t.UserId == demoUserId).ToList();
            var categories = context.Categories.Where(c => c.UserId == demoUserId).ToList();

            context.Expenses.AddRange(
                new Expense
                {
                    TransactionId = transactions.First(t => t.Store == "Supermarket").TransactionId,
                    CategoryId = categories.First(c => c.Name == "Food").CategoryId,
                    Amount = 50,
                    ProductName = "50 kg flour"
                },
                new Expense
                {
                    TransactionId = transactions.First(t => t.Store == "Supermarket").TransactionId,
                    CategoryId = categories.First(c => c.Name == "Food").CategoryId,
                    Amount = 30,
                    ProductName = "Salmon 1 pckg"
                },
                new Expense
                {
                    TransactionId = transactions.First(t => t.Store == "Gas Station").TransactionId,
                    CategoryId = categories.First(c => c.Name == "Transport").CategoryId,
                    Amount = 40,
                    ProductName = "Fuel"
                }
            );

            await context.SaveChangesAsync();
        }
    }
}
