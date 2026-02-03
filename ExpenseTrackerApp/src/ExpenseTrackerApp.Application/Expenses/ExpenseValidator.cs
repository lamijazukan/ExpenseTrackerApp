using ErrorOr;
using ExpenseTrackerApp.Domain.Errors;

namespace ExpenseTrackerApp.Application.Expenses;

public class ExpenseValidator
{
    public static ErrorOr<Success> ValidateCreateExpense(
        decimal amount,
        string productName)
    {
        if (amount <= 0)
            return ExpenseErrors.AmountMustBePositive;

        if (string.IsNullOrWhiteSpace(productName))
            return ExpenseErrors.ProductNameRequired;

        return Result.Success;
    }

    public static ErrorOr<Success> ValidateUpdateExpense(
        decimal? amount = null,
        string? productName = null)
    {
        if (amount is not null && amount <= 0)
            return ExpenseErrors.AmountMustBePositive;

        if (productName is not null && string.IsNullOrWhiteSpace(productName))
            return ExpenseErrors.ProductNameRequired;

        return Result.Success;
    }
}