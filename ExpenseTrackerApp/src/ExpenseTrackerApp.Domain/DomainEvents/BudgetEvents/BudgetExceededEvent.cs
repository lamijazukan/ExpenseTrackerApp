namespace ExpenseTrackerApp.Domain.DomainEvents.BudgetEvents;


public record BudgetExceededEvent(
    Guid UserId,
    int BudgetId,
    decimal SpentAmount,
    decimal BudgetAmount);