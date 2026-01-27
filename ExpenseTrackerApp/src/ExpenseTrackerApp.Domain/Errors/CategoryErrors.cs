using ErrorOr;

namespace ExpenseTrackerApp.Domain.Errors;


public static class CategoryErrors
{
    public static Error NotFound =>
        Error.NotFound(
            $"{nameof(CategoryErrors)}.{nameof(NotFound)}",
            "Category not found");

    public static Error ParentNotFound =>
        Error.NotFound(
            $"{nameof(CategoryErrors)}.{nameof(ParentNotFound)}",
            "Parent category not found");

    public static Error DuplicateName =>
        Error.Conflict(
            $"{nameof(CategoryErrors)}.{nameof(DuplicateName)}",
            "Category with this name already exists");

    public static Error DuplicateNameUnderSameParent =>
        Error.Conflict(
            $"{nameof(CategoryErrors)}.{nameof(DuplicateNameUnderSameParent)}",
            "Category with this name already exists under the same parent");

    public static Error InvalidName =>
        Error.Validation(
            $"{nameof(CategoryErrors)}.{nameof(InvalidName)}",
            "Category name is invalid, must be at least 4 characters long");

    public static Error CircularReference =>
        Error.Validation(
            $"{nameof(CategoryErrors)}.{nameof(CircularReference)}",
            "A category cannot be its own parent");

    public static Error HasChildren =>
        Error.Validation($"{nameof(CategoryErrors)}.{nameof(HasChildren)}",
            "This category has children, cannot be deleted");
}
