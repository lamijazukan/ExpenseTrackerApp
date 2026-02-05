using ExpenseTrackerApp.Domain;

namespace ExpenseTrackerApp.Infrastructure.DomainEventDispatcher;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    public Task DispatchAsync<TEvent>(TEvent domainEvent)
        where TEvent : class
    {
        // Later: publish to MediatR, message bus, email, notifications
        //Right now this class does nothing.
        // 
        // It receives the event and throws it into the void.
        return Task.CompletedTask;
    }
}