using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain.Features
{
    public class NotificationTemplate : Entity<NotificationTemplateId>
    {
        /// <summary>
        /// Unique ID of the <see cref="NotificationTemplate"/>
        /// </summary>
        public override NotificationTemplateId Id => NotificationTemplateId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Indicates the type of the <see cref="Notification"/>
        /// </summary>
        public NotificationType Type { get; init; }

        /// <summary>
        /// Title of the <see cref="NotificationTemplate"/>
        /// </summary>
        public required NotificationTitle Title { get; init; }

        /// <summary>
        /// Contents of the <see cref="NotificationTemplate"/>
        /// </summary>
        public required Text Content { get; init; }

        /// <summary>
        /// Collection of users the <see cref="NotificationTemplate"/> has been sent to
        /// </summary>
        public List<AssociationMember> DeliveredTo { get; private set; } = new List<AssociationMember>();

        /// <summary>
        /// Date and time the <see cref="NotificationTemplate"/> was published
        /// </summary>
        public DateTimeOffset? PublishedDate { get; private set; }

        public bool IsPublished => PublishedDate is not null;

        private NotificationTemplate() { }

        public static NotificationTemplate Create(NotificationTitle title, Text content, NotificationType type)
        {
            return new() { Title = title, Content = content, Type = type };
        }

        public NotificationTemplate Publish(INotificationManager notificationManager)
        {
            if (IsPublished) throw new DomainException(DomainErrors.Notification.AlreadyPublished);

            PublishedDate = notificationManager.SystemClock.UtcNow;

            return this;
        }
    }

    public enum NotificationType
    {
        /// <summary>
        /// Non-urgent informative content
        /// </summary>
        Information,

        /// <summary>
        /// Informs members aboutactions related to transactions
        /// </summary>
        Transaction,

        /// <summary>
        /// Informs members of planned/scheduled events, tasks or activities
        /// </summary>
        Reminder,

        /// <summary>
        /// Conveys critical or time-sensitive information that requires attention
        /// </summary>
        Alert,
    }
}
