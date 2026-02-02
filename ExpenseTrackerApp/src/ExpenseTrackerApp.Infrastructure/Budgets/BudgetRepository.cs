using AutoMapper;
using ErrorOr;
using ExpenseTrackerApp.Application.Budgets.Data;
using ExpenseTrackerApp.Application.Budgets.Interfaces.Infrastructure;
using ExpenseTrackerApp.Domain.Entities;
using ExpenseTrackerApp.Domain.Errors;
using ExpenseTrackerApp.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerApp.Infrastructure.Budgets;

public class BudgetRepository: IBudgetRepository
{
    private readonly AppDbContext _context;

    public BudgetRepository(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    
    //CRUD
    public async Task<ErrorOr<GetBudgetsResult<Budget>>> GetBudgetsByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        try
        {
            var query = _context.Budgets
                .AsNoTracking()
                .Where(b => b.UserId == userId)
                .OrderBy(b => b.StartDate);
                
            var totalCount = await query.CountAsync(cancellationToken);   
            var budgets = await    query.ToListAsync(cancellationToken);
            
            return new GetBudgetsResult<Budget>
            {
                TotalCount = totalCount,
                Budgets = budgets
            };

        }
        catch (Exception ex)
        {
            return Error.Failure("Database.Error", $"Failed to get budgets: {ex.Message}");
        }
    }

    public async Task<ErrorOr<Budget>> GetBudgetByIdAsync(Guid userId, int budgetId, CancellationToken cancellationToken)
    {
        try
        {
            var budget = await _context.Budgets
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.UserId == userId && b.BudgetId == budgetId, cancellationToken);
            
            return budget is null
                ? BudgetErrors.NotFound
                : budget;

        }
        catch (Exception ex)
        {
           return Error.Failure("Database.Error", $"Failed to get budget: {ex.Message}");
        }
        
    }

    public async Task<ErrorOr<Budget>> CreateBudgetAsync(Budget budget, CancellationToken cancellationToken)
    {
        try
        {
            await _context.Budgets.AddAsync(budget, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return budget;

        }
        catch (DbUpdateException ex)
        {
            return Error.Failure("Database.Error", $"Failed to create budget: {ex.Message}");
        }
        
    }

    public async Task<ErrorOr<Budget>> UpdateBudgetAsync(Budget budget, CancellationToken cancellationToken)
    {
        try
        {
            _context.Budgets.Update(budget);
            await _context.SaveChangesAsync(cancellationToken);
            
            return budget;

        }
        catch (DbUpdateException ex)
        {
            return Error.Failure("Database.Error", $"Failed to update budget: {ex.Message}");
        }
        
    }

    public async Task<ErrorOr<Success>> DeleteBudgetAsync(Budget budget, CancellationToken cancellationToken)
    {
        try
        {
            _context.Budgets.Remove(budget);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Result.Success;

        }
        catch (DbUpdateException ex)
        {
            return Error.Failure("Database.Error", $"Failed to delete budget: {ex.Message}");
        }
        
    }
    
    
    //statistics need
    
    public async Task<ErrorOr<Budget>> GetBudgetByCategoryIdAsync(Guid userId, int categoryId, CancellationToken cancellationToken)
    {
        try
        {
            var budget = await _context.Budgets
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.UserId == userId && b.CategoryId == categoryId, cancellationToken);
            
            return budget is null
                ? BudgetErrors.NotFound
                : budget;


        }
        catch (Exception ex)
        {
            return Error.Failure("Database.Error", $"Failed to get budget by category: {ex.Message}");
        }
    }

    
}