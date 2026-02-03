using AutoMapper;
using ErrorOr;
using ExpenseTrackerApp.Application.Transactions.Data;
using ExpenseTrackerApp.Application.Transactions.Interfaces.Application;
using ExpenseTrackerApp.Application.Transactions.Interfaces.Infrastructure;
using ExpenseTrackerApp.Application.Users.Interfaces.Infrastructure;
using ExpenseTrackerApp.Domain.Entities;
using ExpenseTrackerApp.Domain.Errors;

namespace ExpenseTrackerApp.Application.Transactions;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;

    public TransactionService(ITransactionRepository transactionRepository, IMapper mapper, ICurrentUser currentUser)
    {
        _transactionRepository = transactionRepository;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<ErrorOr<GetTransactionsResult<TransactionResult>>> GetAllTransactionsAsync(CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        var result = await _transactionRepository.GetTransactionsByUserIdAsync(userId, cancellationToken);

        if (result.IsError)
            return result.Errors;

        return new GetTransactionsResult<TransactionResult>
        {
            Transactions = _mapper.Map<List<TransactionResult>>(result.Value.Transactions),
            TotalCount = result.Value.TotalCount
        };
    }

    public async Task<ErrorOr<TransactionResult>> GetTransactionByIdAsync(int transactionId, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        var result = await _transactionRepository.GetTransactionByIdAsync(userId, transactionId, cancellationToken);

        if (result.IsError)
            return result.Errors;

        return _mapper.Map<TransactionResult>(result.Value);
    }

    public async Task<ErrorOr<TransactionResult>> CreateTransactionAsync(DateOnly paidDate, string store, decimal totalAmount, string paymentMethod, CancellationToken cancellationToken)
    {
        var validation = TransactionValidator.ValidateCreateTransaction(paidDate, store, totalAmount, paymentMethod);
        if (validation.IsError)
            return validation.Errors;

        var transaction = new Transaction
        {
            UserId = _currentUser.UserId,
            PaidDate = paidDate,
            Store = store,
            TotalAmount = totalAmount,
            PaymentMethod = paymentMethod,
            CreatedAt = DateTime.UtcNow
        };

        var result = await _transactionRepository.CreateTransactionAsync(transaction, cancellationToken);

        if (result.IsError)
            return result.Errors;

        return _mapper.Map<TransactionResult>(result.Value);
    }

    public async Task<ErrorOr<TransactionResult>> UpdateTransactionAsync(int transactionId, DateOnly? paidDate, string? store, decimal? totalAmount, string? paymentMethod, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        var transactionResult = await _transactionRepository.GetTransactionByIdAsync(userId, transactionId, cancellationToken);

        if (transactionResult.IsError)
            return transactionResult.Errors;

        var transaction = transactionResult.Value;

        var validation = TransactionValidator.ValidateUpdateTransaction(paidDate, store, totalAmount, paymentMethod);
        if (validation.IsError)
            return validation.Errors;

        // Apply updates only if provided
        if (paidDate.HasValue) transaction.PaidDate = paidDate.Value;
        if (!string.IsNullOrWhiteSpace(store)) transaction.Store = store;
        if (totalAmount.HasValue) transaction.TotalAmount = totalAmount.Value;
        if (!string.IsNullOrWhiteSpace(paymentMethod)) transaction.PaymentMethod = paymentMethod;

        var updated = await _transactionRepository.UpdateTransactionAsync(transaction, cancellationToken);

        if (updated.IsError)
            return updated.Errors;

        return _mapper.Map<TransactionResult>(updated.Value);
    }

    public async Task<ErrorOr<Success>> DeleteTransactionAsync(int transactionId, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        var transactionResult = await _transactionRepository.GetTransactionByIdAsync(userId, transactionId, cancellationToken);

        if (transactionResult.IsError)
            return transactionResult.Errors;

        var deleteResult = await _transactionRepository.DeleteTransactionAsync(transactionResult.Value, cancellationToken);

        if (deleteResult.IsError)
            return deleteResult.Errors;

        return deleteResult;
    }
}
