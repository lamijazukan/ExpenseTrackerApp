using ErrorOr;
using ExpenseTrackerApp.Domain.Errors;
using ExpenseTrackerApp.Domain.Entities;

namespace ExpenseTrackerApp.Application.Transactions;

public class TransactionValidator
{
    public static ErrorOr<Success> ValidateCreateTransaction(
        DateOnly paidDate,
        string store,
        decimal totalAmount,
        string paymentMethod)
    {
       
        if (paidDate > DateOnly.FromDateTime(DateTime.UtcNow))
        {
            return TransactionErrors.PaidDateCannotBeFuture;
        }
        
        if (string.IsNullOrWhiteSpace(store))
        {
            return TransactionErrors.StoreRequired;
        }

        if (totalAmount <= 0)
        {
            return TransactionErrors.AmountMustBePositive;
        }

   
        if (string.IsNullOrWhiteSpace(paymentMethod))
        {
            return TransactionErrors.PaymentMethodRequired;
        }

        return Result.Success;
    }

    public static ErrorOr<Success> ValidateUpdateTransaction(
        DateOnly? paidDate,
        string? store,
        decimal? totalAmount,
        string? paymentMethod)
    {
       
        if (paidDate is not null)  
        {
            if (paidDate > DateOnly.FromDateTime(DateTime.UtcNow))
            {
                return TransactionErrors.PaidDateCannotBeFuture;
            }
       
        }

        if (store is not null)
        {
            if (string.IsNullOrWhiteSpace(store))
            {
                return TransactionErrors.StoreRequired;
            }
        }

        if (totalAmount.HasValue)
        {
            if (totalAmount.Value <= 0)
            {
                return TransactionErrors.AmountMustBePositive;
            }
        }

        if (paymentMethod is not null)
        {
            if (string.IsNullOrWhiteSpace(paymentMethod))
            {
                return TransactionErrors.PaymentMethodRequired;
            }
        }
        
        return Result.Success;
    }
}