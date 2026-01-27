using ErrorOr;
using ExpenseTrackerApp.Domain.Entities;

namespace ExpenseTrackerApp.Application.Categories.Interfaces.Infrastructure;

public interface ICategoryRepository
{
    Task<ErrorOr<List<Category>>> GetCategoriesByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    
    Task<ErrorOr<Category>> GetCategoryByIdAsync(int categoryId, Guid userId, CancellationToken cancellationToken);
    
    Task<ErrorOr<Category>> CreateCategoryAsync(Category category, CancellationToken cancellationToken);
    Task<ErrorOr<Category>> UpdateCategoryAsync(Category category, CancellationToken cancellationToken);
    Task<ErrorOr<Success>> DeleteCategoryAsync(Category category, CancellationToken cancellationToken);
}