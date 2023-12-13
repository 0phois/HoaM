using HoaM.Domain.Common;

namespace HoaM.Domain
{
    /// <summary>
    /// Represents an event raised when a new notification is created.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="NotificaionCreatedEvent"/> class.
    /// </remarks>
    /// <param name="notification">The created notification associated with the event.</param>
    public sealed class NotificaionCreatedEvent(Notification notification) : DomainEvent
    {
        /// <summary>
        /// Gets the created notification associated with the event.
        /// </summary>
        public Notification Notification { get; } = notification ?? throw new ArgumentNullException(nameof(notification));
    }

}