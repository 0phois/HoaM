using MassTransit;

namespace HoaM.Domain.Common
{
    /// <summary>
    /// Base abstract class for representing domain events in the system.
    /// Domain events are used to model and capture changes or occurrences within the domain.
    /// This class implements the <see cref="INotifyBefore"/> interface to indicate that it supports
    /// notifications before the actual event takes place (is saved).
    /// </summary>
    public abstract class DomainEvent : INotifyBefore
    {
        /// <summary>
        /// Gets the unique identifier for the domain event.
        /// </summary>
        public Guid Id { get; } = NewId.Next().ToGuid();
    }
}
