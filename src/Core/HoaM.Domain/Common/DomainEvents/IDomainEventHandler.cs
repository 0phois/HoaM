namespace HoaM.Domain.Common
{
    /// <summary>
    /// Interface for a domain event handler responsible for processing a specific type of domain event.
    /// </summary>
    /// <typeparam name="TDomainEvent">The type of domain event that this handler can process.</typeparam>
    public interface IDomainEventHandler<in TDomainEvent>
    {
        /// <summary>
        /// Handles the specified domain event asynchronously.
        /// </summary>
        /// <param name="domainEvent">The domain event to be handled.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken);
    }

}
