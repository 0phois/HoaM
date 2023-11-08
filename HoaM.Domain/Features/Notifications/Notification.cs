using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain.Features
{
    public sealed class Notification : Entity<NotificationId>
    {
        /// <summary>
        /// Unique ID of the <see cref="Notification"/>
        /// </summary>
        public override NotificationId Id => NotificationId.From(NewId.Next().ToGuid());

        /// <summary>
        /// The <see cref="NotificationTemplate"/> that generated this <see cref="Notification"/>
        /// </summary>
        public required NotificationTemplate Template { get; init; }

        /// <summary>
        /// The <see cref="AssociationMember"/> the notification was created for
        /// </summary>
        public AssociationMember? Recipient { get; init; }

        /// <summary>
        /// Date and time the <see cref="Notification"/> was received
        /// </summary>
        public DateTimeOffset? ReceivedDate { get; private set; }

        /// <summary>
        /// Date and time the <see cref="Notification"/> was read
        /// </summary>
        public DateTimeOffset ReadDate { get; private set; } = DateTimeOffset.MinValue;

        /// <summary>
        /// Indicates whether the <see cref="Notification"/> has been viewed
        /// </summary>
        public bool IsRead => ReadDate != DateTimeOffset.MinValue;

        private Notification() { }

        public static Notification Create(NotificationTemplate template, AssociationMember recipient)
        {
            if (template is null) throw new DomainException(DomainErrors.NotificationTemplate.NullOrEmpty);

            if (recipient is null) throw new DomainException(DomainErrors.AssociationMember.NullOrEmpty);

            var notification = new Notification() { Template = template, Recipient = recipient };

            notification.AddDomainEvent(new NotificaionCreatedEvent(notification));

            return notification;
        }

        public void MarkAsDelivered(DateTimeOffset dateDelivered)
        {
            if (dateDelivered == default) throw new DomainException(DomainErrors.Notification.DateNullOrEmpty);

            ReceivedDate = dateDelivered;
        }

        public void MarkAsRead(DateTimeOffset dateRead)
        {
            if (dateRead == default) throw new DomainException(DomainErrors.Notification.DateNullOrEmpty);

            ReadDate = dateRead;
        }

        public void MarkAsUnread() => ReadDate = DateTimeOffset.MinValue;
    }
}
