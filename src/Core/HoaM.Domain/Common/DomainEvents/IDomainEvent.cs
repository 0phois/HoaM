namespace HoaM.Domain.Common
{
    /// <summary>
    /// Interface representing a domain event in the system.
    /// A domain event is a representation of a change or occurrence within the domain.
    /// </summary>
    public interface IDomainEvent
    {
        /// <summary>
        /// Gets the unique identifier for the domain event.
        /// </summary>
        Guid Id { get; }
    }

    /// <summary>
    /// Interface indicating that the implementing class supports notifications before the actual event takes place.
    /// Extends the <see cref="IDomainEvent"/> interface.
    /// </summary>
    public interface INotifyBefore : IDomainEvent { }

    /// <summary>
    /// Interface indicating that the implementing class supports notifications after the actual event has taken place.
    /// Extends the <see cref="IDomainEvent"/> interface.
    /// </summary>
    public interface INotifyAfter : IDomainEvent { }
}
