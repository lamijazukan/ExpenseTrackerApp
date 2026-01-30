using ErrorOr;
using ExpenseTrackerApp.Application.Categories.Data;
using ExpenseTrackerApp.Domain.Entities;

namespace ExpenseTrackerApp.Application.Categories.Interfaces.Infrastructure;

public interface ICategoryRepository
{
    Task<ErrorOr<GetCategoriesResult<Category>>> GetCategoriesByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    
    Task<ErrorOr<Category>> GetCategoryByIdAsync(int categoryId, Guid userId, CancellationToken cancellationToken);
    
    Task<ErrorOr<Category>> CreateCategoryAsync(Category category, CancellationToken cancellationToken);
    Task<ErrorOr<Category>> UpdateCategoryAsync(Category category, CancellationToken cancellationToken);
    Task<ErrorOr<Success>> DeleteCategoryAsync(Category category, CancellationToken cancellationToken);
}