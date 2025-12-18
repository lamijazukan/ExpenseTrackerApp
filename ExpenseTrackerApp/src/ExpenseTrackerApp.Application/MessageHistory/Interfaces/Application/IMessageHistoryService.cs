using ErrorOr;
using ExpenseTrackerApp.Application.MessageHistory.Data;

namespace ExpenseTrackerApp.Application.MessageHistory.Interfaces.Application;

public interface IMessageHistoryService
{
    Task<ErrorOr<GetMessageHistoryResult>> GetMessageHistoryAsync(DateTimeOffset startTime, DateTimeOffset endTime, CancellationToken token);
}