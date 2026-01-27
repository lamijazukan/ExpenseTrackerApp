using ErrorOr;
using ExpenseTrackerApp.Application.Categories.Data;
using ExpenseTrackerApp.Domain.Entities;


namespace ExpenseTrackerApp.Application.Categories.Interfaces.Application;

public interface ICategoryService
{
  Task<ErrorOr<GetCategoriesResult>> GetCategoriesAsync(CancellationToken cancellationToken);

  Task<ErrorOr<List<CategoryTreeResult>>> GetCategoryTreeAsync(CancellationToken cancellationToken);

  Task<ErrorOr<CategoryResult>> GetCategoryByIdAsync(int categoryId, CancellationToken cancellationToken);
  
  Task<ErrorOr<CategoryResult>> CreateCategoryAsync(string name, int? parentCategoryId, CancellationToken cancellationToken);
  
  Task<ErrorOr<CategoryResult>> UpdateCategoryAsync(int categoryId, string? name, int? parentCategoryId, CancellationToken cancellationToken);
  
  Task<ErrorOr<Success>> DeleteCategoryAsync(int categoryId, CancellationToken cancellationToken);
}