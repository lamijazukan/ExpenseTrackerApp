using ErrorOr;
using ExpenseTrackerApp.Application.Categories.Data;
using ExpenseTrackerApp.Application.Categories.Interfaces.Infrastructure;
using ExpenseTrackerApp.Domain.Entities;
using ExpenseTrackerApp.Domain.Errors;
using ExpenseTrackerApp.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerApp.Infrastructure.Categories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

     public async Task<ErrorOr<GetCategoriesResult<Category>>> GetCategoriesByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        try
        {
            var query = _context.Categories
                .AsNoTracking()
                .Where(c => c.UserId == userId)
                .OrderBy(c => c.Name);
               
            
            var totalCount = await query.CountAsync(cancellationToken);
            var categories = await query.ToListAsync(cancellationToken);

            
            return new GetCategoriesResult<Category>
            {
                Categories = categories,
                TotalCount = totalCount
            };
        }
        catch (Exception ex)
        {
            return Error.Failure("Database.Error", $"Failed to retrieve categories: {ex.Message}");
        }
    }

    // Get category by id
    public async Task<ErrorOr<Category>> GetCategoryByIdAsync(int categoryId, Guid userId, CancellationToken cancellationToken)
    {
        try
        {
            var category = await _context.Categories
                .AsNoTracking()
                .Include(c => c.ParentCategory)
                .Include(c => c.ChildrenCategories)
                .FirstOrDefaultAsync(c => c.CategoryId == categoryId && c.UserId == userId, cancellationToken);
            
            
            return category is null 
                ? CategoryErrors.NotFound
                : category;
        }
        catch (Exception ex)
        {
            return Error.Failure("Database.Error", $"Failed to retrieve category: {ex.Message}");
        }
    }
    

    // Create category
    public async Task<ErrorOr<Category>> CreateCategoryAsync(Category category, CancellationToken cancellationToken)
    {
        try
        {
            await _context.Categories.AddAsync(category, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return category;
        }
        catch (DbUpdateException ex)
        {
            return Error.Failure("Database.Error", $"Failed to create category: {ex.Message}");
        }
    }

    // Update category
    public async Task<ErrorOr<Category>> UpdateCategoryAsync(Category category, CancellationToken cancellationToken)
    {
        try
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync(cancellationToken);

            return category;
        }
        catch (DbUpdateException ex)
        {
            return Error.Failure("Database.Error", $"Failed to update category: {ex.Message}");
        }
    }

    // Delete category
    public async Task<ErrorOr<Success>> DeleteCategoryAsync(Category category, CancellationToken cancellationToken)
    {
        try
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
        catch (DbUpdateException ex)
        {
            return Error.Failure("Database.Error", $"Failed to delete category: {ex.Message}");
        }
    }
    
}   