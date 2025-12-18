using ErrorOr;
using ExpenseTrackerApp.Application.MessageHistory.Data;
using ExpenseTrackerApp.Application.MessageHistory.Interfaces.Infrastructure;
using ExpenseTrackerApp.Infrastructure.MessageHistory.Options;

namespace ExpenseTrackerApp.Infrastructure.MessageHistory;

public class MessageHistoryRepository : IMessageHistoryRepository
{
    private readonly MessageHistoryOptions _options;

    public MessageHistoryRepository(MessageHistoryOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task<ErrorOr<List<MessageHistoryRecord>>> GetMessageHistoryAsync(DateTimeOffset startTime,
        DateTimeOffset endTime, CancellationToken token)
    {
        // Example 
        await Task.Delay(100, token);

        // As an example, get the database url
        var databaseUrl = _options.DatabaseUrl;

        // Query the database to retrieve the message history records

        // For demonstration purposes, return an empty list.
        // In the real implementation, you would query your data source
        return new List<MessageHistoryRecord>();
    }
}
   