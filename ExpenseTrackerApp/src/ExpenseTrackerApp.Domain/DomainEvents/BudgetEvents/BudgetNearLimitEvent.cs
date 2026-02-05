namespace ExpenseTrackerApp.Domain.DomainEvents.BudgetEvents;

public record BudgetNearLimitEvent(
    Guid UserId,
    int BudgetId,
    decimal SpentAmount,
    decimal BudgetAmount);