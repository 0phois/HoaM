using MassTransit;

namespace HoaM.Domain.Common
{
    /// <summary>
    /// Base abstract class for representing domain notifications in the system.
    /// Domain notifications are used to signal events that have occurred after an action or state change in the domain.
    /// This class implements the <see cref="INotifyAfter"/> interface to indicate that it supports
    /// notifications after the actual event has taken place (been saved).
    /// </summary>
    public abstract class DomainNotification : INotifyAfter
    {
        /// <summary>
        /// Gets the unique identifier for the domain notification.
        /// </summary>
        public Guid Id { get; } = NewId.Next().ToGuid();
    }
}
