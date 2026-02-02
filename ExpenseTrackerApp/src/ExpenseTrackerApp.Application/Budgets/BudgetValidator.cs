using ErrorOr;
using ExpenseTrackerApp.Domain.Entities;
using ExpenseTrackerApp.Domain.Errors;

namespace ExpenseTrackerApp.Application.Budgets;

public class BudgetValidator
{
    public static ErrorOr<Success> ValidateAmountAndDate(decimal? amount, DateOnly? startDate, DateOnly? endDate)
    {
        if (amount <= 0)
        {
            return BudgetErrors.AmountMustBePositive;
        }

        if (endDate <= startDate)
        {
            return BudgetErrors.InvalidDateRange;
        }

        return Result.Success;
    }

} 