using ErrorOr;

namespace ExpenseTrackerApp.Domain.Errors;

public class BudgetErrors
{
    public static Error AmountMustBePositive =>
        Error.Validation("Budget.AmountMustBePositive", "Budget amount must be greater than zero.");

    public static Error InvalidDateRange =>
        Error.Validation("Budget.InvalidDateRange", "End date must be after start date.");

    public static Error BudgetAlreadyExistsForCategory =>
        Error.Conflict("Budget.AlreadyExists", "A budget already exists for this category and period.");

    public static Error BudgetOverlapsExisting =>
        Error.Conflict("Budget.Overlaps", "Budget period overlaps an existing budget.");

    public static Error NotFound =>
        Error.NotFound("Budget.NotFound", "Budget not found.");
    
}