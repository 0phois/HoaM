using HoaM.Domain.Common;
using MassTransit;

namespace HoaM.Domain.Entities
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
        public NotificationType Type { get; set; }

        /// <summary>
        /// Title of the <see cref="NotificationTemplate"/>
        /// </summary>
        public required NotificationTitle Title { get; set; }

        /// <summary>
        /// Contents of the <see cref="NotificationTemplate"/>
        /// </summary>
        public required Text Content { get; set; }

        /// <summary>
        /// Collection of users the <see cref="NotificationTemplate"/> has been sent to
        /// </summary>
        public List<AssociationMember> DeliveredTo { get; set; } = new List<AssociationMember>();

        /// <summary>
        /// Date and time the <see cref="NotificationTemplate"/> was published
        /// </summary>
        public DateTimeOffset PublishedDate { get; private set; }
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
