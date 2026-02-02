using AutoMapper;
using ErrorOr;
using ExpenseTrackerApp.Application.Budgets.Data;
using ExpenseTrackerApp.Application.Budgets.Interfaces.Application;
using ExpenseTrackerApp.Application.Budgets.Interfaces.Infrastructure;
using ExpenseTrackerApp.Application.Users.Interfaces.Infrastructure;
using ExpenseTrackerApp.Domain.Entities;

namespace ExpenseTrackerApp.Application.Budgets;

public class BudgetService: IBudgetService
{
    private readonly IBudgetRepository _budgetRepository;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;

    public BudgetService(IBudgetRepository budgetRepository, IMapper mapper, ICurrentUser currentUser)
    {
        _budgetRepository = budgetRepository;
        _mapper = mapper;
        _currentUser = currentUser;
    }
    
    public async Task<ErrorOr<GetBudgetsResult<BudgetResult>>> GetAllBudgetsAsync(CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        var result = await _budgetRepository.GetBudgetsByUserIdAsync(userId, cancellationToken);

        if (result.IsError)
        {
            return result.Errors;
        }

        return new GetBudgetsResult<BudgetResult>
        {
            Budgets = _mapper.Map<List<BudgetResult>>(result.Value.Budgets),
            TotalCount = result.Value.TotalCount,
        };

    }
    
    public async Task<ErrorOr<BudgetResult>> GetBudgetByIdAsync (int budgetId, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        var result = await _budgetRepository.GetBudgetByIdAsync(userId, budgetId, cancellationToken);

        if (result.IsError)
        {
            return result.Errors;
        }

        return _mapper.Map<BudgetResult>(result.Value);
    }

    public async Task<ErrorOr<BudgetResult>> GetBudgetByCategoryIdAsync(int categoryId, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        var result = await _budgetRepository.GetBudgetByCategoryIdAsync(userId, categoryId, cancellationToken);

        if (result.IsError)
        {
            return result.Errors;
        }
        
        return _mapper.Map<BudgetResult>(result.Value);
    }

    public async Task<ErrorOr<BudgetResult>> CreateBudgetAsync(int categoryId, decimal amount, DateOnly startDate,
        DateOnly endDate, CancellationToken cancellationToken)
    {
        var validation = BudgetValidator.ValidateAmountAndDate(amount, startDate, endDate);
        if (validation.IsError)
            return validation.Errors;
        
        var userId = _currentUser.UserId;

        var budget = new Budget
        {
            UserId = userId,
            CategoryId = categoryId,
            Amount = amount,
            StartDate = startDate,
            EndDate = endDate,
           
        };
        
        var result = await _budgetRepository.CreateBudgetAsync(budget, cancellationToken);

        if (result.IsError)
        {
            return result.Errors;
        }
        
        return _mapper.Map<BudgetResult>(result.Value);
            
    }

    public async Task<ErrorOr<BudgetResult>> UpdateBudgetAsync(int budgetId, decimal? amount, DateOnly? startDate,
        DateOnly? endDate, CancellationToken cancellationToken)
    {
        var validation = BudgetValidator.ValidateAmountAndDate(amount, startDate, endDate);
        if (validation.IsError)
            return validation.Errors;
        
        var userId = _currentUser.UserId;
        var budgetResult = await _budgetRepository.GetBudgetByIdAsync(userId, budgetId, cancellationToken);

        if (budgetResult.IsError)
        {
            return budgetResult.Errors;
        }
        
        var budget = budgetResult.Value;

        if (amount is not null)
        {
            //here left value is non-nullable, nullable cannot be directly assigned so use .Value to unwrap it
            budget.Amount = amount.Value;
        }

        if (startDate is not null)
        {
            budget.StartDate = startDate.Value;
        }

        if (endDate is not null)
        {
            budget.EndDate = endDate.Value;
        }
        
        
        var result = await _budgetRepository.UpdateBudgetAsync(budget, cancellationToken);

        if (result.IsError)
        {
            return result.Errors;
        }
        
        return _mapper.Map<BudgetResult>(result.Value);
    }

    public async Task<ErrorOr<Success>> DeleteBudgetAsync(int budgetId, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        
        var result = await _budgetRepository.GetBudgetByIdAsync(userId, budgetId, cancellationToken);

        if (result.IsError)
        {
            return result.Errors;
        }
        
        var budget = _mapper.Map<Budget>(result.Value);
        
        var deleteResult = await _budgetRepository.DeleteBudgetAsync(budget, cancellationToken);

        if (deleteResult.IsError)
        {
            return deleteResult.Errors;
        }

        return deleteResult;
    }
}