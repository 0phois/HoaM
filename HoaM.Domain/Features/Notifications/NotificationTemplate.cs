using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain
{
    public class NotificationTemplate : Entity<NotificationTemplateId>
    {
        /// <summary>
        /// Unique ID of the <see cref="NotificationTemplate"/>
        /// </summary>
        public override NotificationTemplateId Id { get; protected set; } = NotificationTemplateId.From(NewId.Next().ToGuid());

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

        private NotificationTemplate() { }

        public static NotificationTemplate Create(NotificationTitle title, Text content, NotificationType type)
        {
            if (title is null) throw new DomainException(DomainErrors.NotificationTemplate.TitleNullOrEmpty);

            if (content is null) throw new DomainException(DomainErrors.NotificationTemplate.ContentNullOrEmpty);

            if (!Enum.IsDefined(typeof(NotificationType), type)) throw new DomainException(DomainErrors.NotificationTemplate.TypeNotDefined);

            return new() { Title = title, Content = content, Type = type };
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
