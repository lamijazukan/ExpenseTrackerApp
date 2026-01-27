using ErrorOr;
using ExpenseTrackerApp.Domain.Entities;
using ExpenseTrackerApp.Domain.Errors;

namespace ExpenseTrackerApp.Application.Categories;

public class CategoryValidator
{
    public static ErrorOr<Success> ValidateCreateCategoryRequest(
        string name,
        Category? parentCategory,
        IEnumerable<Category>? siblingCategories = null)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length < 4 || name.Length > 50)
        {
            return CategoryErrors.InvalidName;
        }

        // Check duplicates among siblings (same parent)
        if (siblingCategories != null &&
            siblingCategories.Any(c =>
                c.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
        {
            return CategoryErrors.DuplicateNameUnderSameParent;
        }

        return Result.Success;
    }
    public static ErrorOr<Success> ValidateUpdateCategoryRequest(
        Category category,
        string? newName,
        Category? newParentCategory,
        IEnumerable<Category>? siblingCategories = null)
    {
        // Validate name ONLY if provided
        if (newName != null)
        {
            if (string.IsNullOrWhiteSpace(newName) || newName.Length < 4 || newName.Length > 50)
            {
                return CategoryErrors.InvalidName;
            }

            if (siblingCategories != null &&
                siblingCategories.Any(c =>
                    c.CategoryId != category.CategoryId &&
                    c.Name.Equals(newName, StringComparison.OrdinalIgnoreCase)))
            {
                return CategoryErrors.DuplicateNameUnderSameParent;
            }
        }

        // Validate parent ONLY if provided
        if (newParentCategory != null)
        {
            // Cannot be its own parent
            if (newParentCategory.CategoryId == category.CategoryId)
            {
                return CategoryErrors.CircularReference;
            }
        }

        return Result.Success;
    }

    public static ErrorOr<Success> ValidateDeleteCategoryRequest(Category category)
    {
        if (category.ChildrenCategories.Any())
        {
            return CategoryErrors.HasChildren;
        }

        return Result.Success;
    }



}