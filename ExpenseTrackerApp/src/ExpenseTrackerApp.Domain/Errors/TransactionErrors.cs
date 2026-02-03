using ErrorOr;

namespace ExpenseTrackerApp.Domain.Errors;

public class TransactionErrors
{
    public static Error AmountMustBePositive =>
        Error.Validation(
            "Transaction.AmountMustBePositive",
            "Transaction total amount must be greater than zero.");

    public static Error PaidDateCannotBeFuture =>
        Error.Validation(
            "Transaction.PaidDateCannotBeFuture",
            "Transaction paid date cannot be in the future.");

    public static Error StoreRequired =>
        Error.Validation(
            "Transaction.StoreRequired",
            "Store name is required.");

    public static Error PaymentMethodRequired =>
        Error.Validation(
            "Transaction.PaymentMethodRequired",
            "Payment method is required.");

    public static Error DuplicateTransaction =>
        Error.Validation(
            "Transaction.Duplicate",
            "Same transaction already exists.");

    public static Error NotFound =>
        Error.NotFound(
            "Transaction.NotFound",
            "Transaction not found.");
    
}