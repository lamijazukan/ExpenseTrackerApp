using AutoMapper;
using ErrorOr;
using ExpenseTrackerApp.Application.Transactions.Data;
using ExpenseTrackerApp.Application.Transactions.Interfaces.Infrastructure;
using ExpenseTrackerApp.Domain.Entities;
using ExpenseTrackerApp.Domain.Errors;
using ExpenseTrackerApp.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerApp.Infrastructure.Transactions;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<ErrorOr<GetTransactionsResult<Transaction>>> GetTransactionsByUserIdAsync(
        Guid userId, CancellationToken cancellationToken)
    {
        try
        {
            var query = _context.Transactions
                .AsNoTracking()
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.PaidDate);

            var totalCount = await query.CountAsync(cancellationToken);
            var transactions = await query.ToListAsync(cancellationToken);

            return new GetTransactionsResult<Transaction>
            {
                TotalCount = totalCount,
                Transactions = transactions
            };
        }
        catch (Exception ex)
        {
            return Error.Failure("Database.Error", $"Failed to get transactions: {ex.Message}");
        }
    }

    public async Task<ErrorOr<Transaction>> GetTransactionByIdAsync(
        Guid userId, int transactionId, CancellationToken cancellationToken)
    {
        try
        {
            var transaction = await _context.Transactions
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.UserId == userId && t.TransactionId == transactionId, cancellationToken);

            return transaction is null
                ? TransactionErrors.NotFound
                : transaction;
        }
        catch (Exception ex)
        {
            return Error.Failure("Database.Error", $"Failed to get transaction: {ex.Message}");
        }
    }

    public async Task<ErrorOr<GetTransactionsResult<Transaction>>> GetTransactionsByDateRangeAsync(
        Guid userId, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken)
    {
        try
        {
            var query =  _context.Transactions
                .AsNoTracking()
                .Where(t => t.UserId == userId && t.PaidDate >= startDate && t.PaidDate <= endDate)
                .OrderBy(t => t.PaidDate);
            
            var totalCount = await query.CountAsync(cancellationToken);
            var transactions = await query.ToListAsync(cancellationToken);


            return new GetTransactionsResult<Transaction>
            {
                Transactions = transactions,
                TotalCount = totalCount
            };
        }
        catch (Exception ex)
        {
            return Error.Failure("Database.Error", $"Failed to get transactions by date range: {ex.Message}");
        }
    }

    public async Task<ErrorOr<Transaction>> CreateTransactionAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        try
        {
            await _context.Transactions.AddAsync(transaction, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return transaction;
        }
        catch (DbUpdateException ex)
        {
            return Error.Failure("Database.Error", $"Failed to create transaction: {ex.Message}");
        }
    }

    public async Task<ErrorOr<Transaction>> UpdateTransactionAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        try
        {
            _context.Transactions.Update(transaction);
            await _context.SaveChangesAsync(cancellationToken);

            return transaction;
        }
        catch (DbUpdateException ex)
        {
            return Error.Failure("Database.Error", $"Failed to update transaction: {ex.Message}");
        }
    }

    public async Task<ErrorOr<Success>> DeleteTransactionAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        try
        {
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
        catch (DbUpdateException ex)
        {
            return Error.Failure("Database.Error", $"Failed to delete transaction: {ex.Message}");
        }
    }
}
