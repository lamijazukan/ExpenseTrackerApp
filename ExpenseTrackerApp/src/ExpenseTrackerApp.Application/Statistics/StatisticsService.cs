using AutoMapper;
using ErrorOr;
using ExpenseTrackerApp.Application.Budgets.Interfaces.Infrastructure;
using ExpenseTrackerApp.Application.Expenses.Interfaces.Infrastructure;
using ExpenseTrackerApp.Application.Statistics.Data;
using ExpenseTrackerApp.Application.Statistics.Interfaces.Application;
using ExpenseTrackerApp.Application.Users.Interfaces.Infrastructure;
using ExpenseTrackerApp.Domain;
using ExpenseTrackerApp.Domain.DomainEvents.BudgetEvents;
using ExpenseTrackerApp.Domain.DomainServices;

namespace ExpenseTrackerApp.Application.Statistics;

public class StatisticsService: IStatisticService
{
    private readonly IBudgetRepository _budgetRepository;
    private readonly IExpenseRepository _expenseRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IDomainEventDispatcher _eventDispatcher;
    private readonly IMapper _mapper;

    public StatisticsService(
        IBudgetRepository budgetRepository,
        IExpenseRepository expenseRepository,
        ICurrentUser currentUser,
        IDomainEventDispatcher eventDispatcher,
        IMapper mapper)
    {
        _budgetRepository = budgetRepository;
        _expenseRepository = expenseRepository;
        _currentUser = currentUser;
        _eventDispatcher = eventDispatcher;
        _mapper = mapper;
    }

    public async Task<ErrorOr<BudgetStatusResult>> GetBudgetStatusAsync(
        int budgetId,
        CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;

        var budgetResult = await _budgetRepository.GetBudgetByIdAsync(
            userId, budgetId, cancellationToken);

        if (budgetResult.IsError)
            return budgetResult.Errors;

        var budget = budgetResult.Value;

        var spentResult =
            await _expenseRepository.GetTotalExpensesForBudgetPeriodAsync(
                userId,
                budget.CategoryId,
                budget.StartDate,
                budget.EndDate,
                cancellationToken);

        if (spentResult.IsError)
            return spentResult.Errors;

        var status = BudgetStatusCalculator.Calculate(
            budget.Amount,
            spentResult.Value);

        
        if (status.IsExceeded)
        {
            await _eventDispatcher.DispatchAsync(
                new BudgetExceededEvent(
                    userId,
                    budget.BudgetId,
                    status.SpentAmount,
                    status.TotalAmount));
        }
        else if (status.IsNearLimit)
        {
            await _eventDispatcher.DispatchAsync(
                new BudgetNearLimitEvent(
                    userId,
                    budget.BudgetId,
                    status.SpentAmount,
                    status.TotalAmount));
        }

        return _mapper.Map<BudgetStatusResult>(status);
    }
    
    public async Task<ErrorOr<TotalExpenseByCategoryResult>> GetTotalExpenseByCategoryAsync(
        int categoryId,
        DateOnly from,
        DateOnly to,
        CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;

        var totalResult =
            await _expenseRepository.GetTotalExpensesForBudgetPeriodAsync(
                userId,
                categoryId,
                from,
                to,
                cancellationToken);

        if (totalResult.IsError)
            return totalResult.Errors;

        return new TotalExpenseByCategoryResult
        {
            CategoryId = categoryId,
            From = from,
            To = to,
            TotalAmount = totalResult.Value
        };
    }
    
    public async Task<ErrorOr<MonthlySavingsResult>> GetMonthlySavingsAsync(
        int year,
        int month,
        CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;

        var expensesResult =
            await _expenseRepository.GetTotalExpensesForMonthAsync(
                userId,
                year,
                month,
                cancellationToken);

        if (expensesResult.IsError)
            return expensesResult.Errors;

        var budgetResult =
            await _budgetRepository.GetTotalBudgetForMonthAsync(
                userId,
                year,
                month,
                cancellationToken);

        if (budgetResult.IsError)
            return budgetResult.Errors;

        var totalExpenses = expensesResult.Value;
        var totalBudget = budgetResult.Value;

        return new MonthlySavingsResult
        {
            Year = year,
            Month = month,
            BudgetBalance = totalBudget,    
            Expenses = totalExpenses,
            Savings = totalBudget - totalExpenses
        };
    }

}
