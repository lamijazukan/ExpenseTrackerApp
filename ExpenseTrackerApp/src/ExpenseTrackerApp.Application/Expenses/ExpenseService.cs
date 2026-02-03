using AutoMapper;
using ErrorOr;
using ExpenseTrackerApp.Application.Expenses.Data;
using ExpenseTrackerApp.Application.Expenses.Interfaces.Application;
using ExpenseTrackerApp.Application.Expenses.Interfaces.Infrastructure;
using ExpenseTrackerApp.Application.Users.Interfaces.Infrastructure;
using ExpenseTrackerApp.Domain.Entities;
using ExpenseTrackerApp.Domain.Errors;

namespace ExpenseTrackerApp.Application.Expenses;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;

    public ExpenseService(IExpenseRepository expenseRepository, IMapper mapper, ICurrentUser currentUser)
    {
        _expenseRepository = expenseRepository;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<ErrorOr<GetExpensesResult<ExpenseResult>>> GetAllExpensesAsync(CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        var result = await _expenseRepository.GetExpensesByUserIdAsync(userId, cancellationToken);

        if (result.IsError)
            return result.Errors;

        return new GetExpensesResult<ExpenseResult>
        {
            Expenses = _mapper.Map<List<ExpenseResult>>(result.Value.Expenses),
            TotalCount = result.Value.TotalCount
        };
    }

    public async Task<ErrorOr<ExpenseResult>> GetExpenseByIdAsync(int expenseId, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        var result = await _expenseRepository.GetExpenseByIdAsync(userId, expenseId, cancellationToken);

        if (result.IsError)
            return result.Errors;

        return _mapper.Map<ExpenseResult>(result.Value);
    }

    public async Task<ErrorOr<ExpenseResult>> CreateExpenseAsync(int transactionId, int categoryId, decimal amount, string productName, CancellationToken cancellationToken)
    {
        var validation = ExpenseValidator.ValidateCreateExpense(amount, productName);
        if (validation.IsError)
            return validation.Errors;

        var expense = new Expense
        {
            TransactionId = transactionId,
            CategoryId = categoryId,
            Amount = amount,
            ProductName = productName
        };

        var result = await _expenseRepository.CreateExpenseAsync(expense, cancellationToken);

        if (result.IsError)
            return result.Errors;

        return _mapper.Map<ExpenseResult>(result.Value);
    }

    public async Task<ErrorOr<ExpenseResult>> UpdateExpenseAsync(int expenseId, int? transactionId, int? categoryId, decimal? amount, string? productName, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        var expenseResult = await _expenseRepository.GetExpenseByIdAsync(userId, expenseId, cancellationToken);

        if (expenseResult.IsError)
            return expenseResult.Errors;

        var expense = expenseResult.Value;

        var validation = ExpenseValidator.ValidateUpdateExpense(amount, productName);
        if (validation.IsError)
            return validation.Errors;

        if (transactionId.HasValue) expense.TransactionId = transactionId.Value;
        if (categoryId.HasValue) expense.CategoryId = categoryId.Value;
        if (amount.HasValue) expense.Amount = amount.Value;
        if (!string.IsNullOrWhiteSpace(productName)) expense.ProductName = productName;

        var updated = await _expenseRepository.UpdateExpenseAsync(expense, cancellationToken);

        if (updated.IsError)
            return updated.Errors;

        return _mapper.Map<ExpenseResult>(updated.Value);
    }

    public async Task<ErrorOr<Success>> DeleteExpenseAsync(int expenseId, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        var expenseResult = await _expenseRepository.GetExpenseByIdAsync(userId, expenseId, cancellationToken);

        if (expenseResult.IsError)
            return expenseResult.Errors;

        var deleteResult = await _expenseRepository.DeleteExpenseAsync(expenseResult.Value, cancellationToken);

        if (deleteResult.IsError)
            return deleteResult.Errors;

        return deleteResult;
    }
}
