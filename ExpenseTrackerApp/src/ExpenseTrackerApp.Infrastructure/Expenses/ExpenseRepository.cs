using ErrorOr;
using ExpenseTrackerApp.Application.Expenses.Data;
using ExpenseTrackerApp.Application.Expenses.Interfaces.Infrastructure;
using ExpenseTrackerApp.Domain.Entities;
using ExpenseTrackerApp.Domain.Errors;
using ExpenseTrackerApp.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerApp.Infrastructure.Expenses;

public class ExpenseRepository : IExpenseRepository
{
    private readonly AppDbContext _context;

    public ExpenseRepository(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<ErrorOr<GetExpensesResult<Expense>>> GetExpensesByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        try
        {
            var query = _context.Expenses
                .Include(e => e.Transaction)
                .Include(e => e.Category)
                .Where(e => e.Transaction.UserId == userId)
                .AsNoTracking()
                .OrderByDescending(e => e.ExpenseId);

            var totalCount = await query.CountAsync(cancellationToken);
            var expenses = await query.ToListAsync(cancellationToken);

            return new GetExpensesResult<Expense>
            {
                TotalCount = totalCount,
                Expenses = expenses
            };
        }
        catch (Exception ex)
        {
            return Error.Failure("Database.Error", $"Failed to get expenses: {ex.Message}");
        }
    }

    public async Task<ErrorOr<Expense>> GetExpenseByIdAsync(Guid userId, int expenseId, CancellationToken cancellationToken)
    {
        try
        {
            var expense = await _context.Expenses
                .Include(e => e.Transaction)
                .Include(e => e.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.ExpenseId == expenseId && e.Transaction.UserId == userId, cancellationToken);

            return expense is null
                   ? ExpenseErrors.NotFound
                   : expense;
        }
        catch (Exception ex)
        {
            return Error.Failure("Database.Error", $"Failed to get expense: {ex.Message}");
        }
    }

    public async Task<ErrorOr<Expense>> CreateExpenseAsync(Expense expense, CancellationToken cancellationToken)
    {
        try
        {
            await _context.Expenses.AddAsync(expense, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return expense;
        }
        catch (DbUpdateException ex)
        {
            return Error.Failure("Database.Error", $"Failed to create expense: {ex.Message}");
        }
    }

    public async Task<ErrorOr<Expense>> UpdateExpenseAsync(Expense expense, CancellationToken cancellationToken)
    {
        try
        {
            _context.Expenses.Update(expense);
            await _context.SaveChangesAsync(cancellationToken);
            return expense;
        }
        catch (DbUpdateException ex)
        {
            return Error.Failure("Database.Error", $"Failed to update expense: {ex.Message}");
        }
    }

    public async Task<ErrorOr<Success>> DeleteExpenseAsync(Expense expense, CancellationToken cancellationToken)
    {
        try
        {
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success;
        }
        catch (DbUpdateException ex)
        {
            return Error.Failure("Database.Error", $"Failed to delete expense: {ex.Message}");
        }
    }
    
    //for statistics purposes
    
    public async Task<ErrorOr<decimal>> GetTotalExpensesForBudgetPeriodAsync(
        Guid userId,
        int categoryId,
        DateOnly startDate,
        DateOnly endDate,
        CancellationToken cancellationToken)
    {
        try
        {
            var total = await _context.Expenses
                .AsNoTracking()
                .Where(e =>
                    e.CategoryId == categoryId &&
                    e.Transaction.UserId == userId &&
                    e.Transaction.PaidDate >= startDate &&
                    e.Transaction.PaidDate <= endDate)
                .SumAsync(e => e.Amount, cancellationToken);

            return total;
        }
        catch (Exception ex)
        {
            return Error.Failure(
                "Database.Error",
                $"Failed to calculate total expenses for budget period: {ex.Message}");
        }
    }

    public async Task<ErrorOr<decimal>> GetTotalExpensesForMonthAsync(
        Guid userId,
        int year,
        int month,
        CancellationToken cancellationToken)
    {
        try
        {
            var total = await _context.Expenses
                .AsNoTracking()
                .Where(e =>
                    e.Transaction.UserId == userId &&
                    e.Transaction.PaidDate.Year == year &&
                    e.Transaction.PaidDate.Month == month)
                .SumAsync(e => e.Amount, cancellationToken);

            return total;
        }
        catch (Exception ex)
        {
            return Error.Failure(
                "Database.Error",
                $"Failed to calculate monthly expenses: {ex.Message}");
        }
    }
}
