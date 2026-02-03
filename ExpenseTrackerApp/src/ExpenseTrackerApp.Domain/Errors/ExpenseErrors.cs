using ErrorOr;

namespace ExpenseTrackerApp.Domain.Errors;

public class ExpenseErrors
{
    public static Error AmountMustBePositive =>
        Error.Validation("Expense.AmountMustBePositive", "Expense amount must be greater than zero.");

    public static Error ProductNameRequired =>
        Error.Validation("Expense.ProductNameRequired", "Product name must not be empty.");
    

    public static Error NotFound =>
        Error.NotFound("Expense.NotFound", "Expense not found.");

    public static Error DuplicateExpense =>
        Error.Conflict("Expense.Duplicate", "An expense with the same details already exists.");
}