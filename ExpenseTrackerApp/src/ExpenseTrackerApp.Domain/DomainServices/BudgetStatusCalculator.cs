using ExpenseTrackerApp.Domain.Models;

namespace ExpenseTrackerApp.Domain.DomainServices;


public static class BudgetStatusCalculator
{
    private const decimal NearLimitThreshold = 0.8m;

    public static BudgetStatus Calculate(
        decimal budgetAmount,
        decimal spentAmount)
    {
        var remaining = budgetAmount - spentAmount;

        return new BudgetStatus
        {
            TotalAmount = budgetAmount,
            SpentAmount = spentAmount,
            RemainingAmount = remaining,
            IsExceeded = spentAmount > budgetAmount,
            IsNearLimit = spentAmount >= budgetAmount * NearLimitThreshold
        };
    }
}