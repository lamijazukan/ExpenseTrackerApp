using ErrorOr;
using ExpenseTrackerApp.Application.MessageHistory.Data;

namespace ExpenseTrackerApp.Application.MessageHistory.Interfaces.Infrastructure;

public interface IMessageHistoryRepository
{
    Task<ErrorOr<List<MessageHistoryRecord>>> GetMessageHistoryAsync(DateTimeOffset startTime, DateTimeOffset endTime, CancellationToken token);
}