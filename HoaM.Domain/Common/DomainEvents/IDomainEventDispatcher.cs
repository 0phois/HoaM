namespace HoaM.Domain.Common
{
    /// <summary>
    /// Interface for a domain event dispatcher responsible for publishing domain events to interested subscribers.
    /// </summary>
    public interface IDomainEventDispatcher
    {
        /// <summary>
        /// Publishes the specified domain event asynchronously to all interested subscribers.
        /// </summary>
        /// <param name="domainEvent">The domain event to be published.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task PublishAsync(IDomainEvent domainEvent, CancellationToken cancellationToken);
    }
}
