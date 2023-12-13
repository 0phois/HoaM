using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain
{
    /// <summary>
    /// Represents a notification within the system.
    /// </summary>
    public sealed class Notification : Entity<NotificationId>
    {
        /// <inheritdoc />
        public override NotificationId Id { get; protected set; } = NotificationId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Gets or sets the template for the notification.
        /// </summary>
        public required NotificationTemplate Template { get; init; }

        /// <summary>
        /// Gets or sets the recipient of the notification.
        /// </summary>
        public AssociationMember? Recipient { get; init; }

        /// <summary>
        /// Gets or sets the date the notification was received.
        /// </summary>
        public DateTimeOffset? ReceivedDate { get; private set; }

        /// <summary>
        /// Gets or sets the date the notification was read.
        /// </summary>
        public DateTimeOffset ReadDate { get; private set; } = DateTimeOffset.MinValue;

        /// <summary>
        /// Gets a value indicating whether the notification has been read.
        /// </summary>
        public bool IsRead => ReadDate != DateTimeOffset.MinValue;

        /// <summary>
        /// Private parameterless constructor for entity creation.
        /// </summary>
        private Notification() { }

        /// <summary>
        /// Creates a new <see cref="Notification"/> instance.
        /// </summary>
        /// <param name="template">The template for the notification.</param>
        /// <param name="recipient">The recipient of the notification.</param>
        /// <returns>A new instance of the <see cref="Notification"/> class.</returns>
        public static Notification Create(NotificationTemplate template, AssociationMember recipient)
        {
            if (template is null) throw new DomainException(DomainErrors.NotificationTemplate.NullOrEmpty);

            if (recipient is null) throw new DomainException(DomainErrors.AssociationMember.NullOrEmpty);

            var notification = new Notification() { Template = template, Recipient = recipient };

            notification.AddDomainEvent(new NotificaionCreatedEvent(notification));

            return notification;
        }

        /// <summary>
        /// Marks the notification as delivered with the specified delivery date.
        /// </summary>
        /// <param name="dateDelivered">The date the notification was delivered.</param>
        public void MarkAsDelivered(DateTimeOffset dateDelivered)
        {
            if (dateDelivered == default) throw new DomainException(DomainErrors.Notification.DateNullOrEmpty);

            ReceivedDate = dateDelivered;
        }

        /// <summary>
        /// Marks the notification as read with the specified read date.
        /// </summary>
        /// <param name="dateRead">The date the notification was read.</param>
        public void MarkAsRead(DateTimeOffset dateRead)
        {
            if (dateRead == default) throw new DomainException(DomainErrors.Notification.DateNullOrEmpty);

            if (ReceivedDate == default) throw new DomainException(DomainErrors.Notification.NotReceived);

            ReadDate = dateRead;
        }

        /// <summary>
        /// Marks the notification as unread.
        /// </summary>
        public void MarkAsUnread() => ReadDate = DateTimeOffset.MinValue;
    }

}
