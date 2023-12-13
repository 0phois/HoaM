using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain
{
    /// <summary>
    /// Represents a template for notifications within the system.
    /// </summary>
    public class NotificationTemplate : Entity<NotificationTemplateId>
    {
        /// <inheritdoc />
        public override NotificationTemplateId Id { get; protected set; } = NotificationTemplateId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Gets or sets the type of the notification.
        /// </summary>
        public NotificationType Type { get; init; }

        /// <summary>
        /// Gets or sets the title of the notification template.
        /// </summary>
        public required NotificationTitle Title { get; init; }

        /// <summary>
        /// Gets or sets the content of the notification template.
        /// </summary>
        public required Text Content { get; init; }

        /// <summary>
        /// Private parameterless constructor for entity creation.
        /// </summary>
        private NotificationTemplate() { }

        /// <summary>
        /// Creates a new <see cref="NotificationTemplate"/> instance.
        /// </summary>
        /// <param name="title">The title of the notification template.</param>
        /// <param name="content">The content of the notification template.</param>
        /// <param name="type">The type of the notification template.</param>
        /// <returns>A new instance of the <see cref="NotificationTemplate"/> class.</returns>
        public static NotificationTemplate Create(NotificationTitle title, Text content, NotificationType type)
        {
            if (title is null) throw new DomainException(DomainErrors.NotificationTemplate.TitleNullOrEmpty);

            if (content is null) throw new DomainException(DomainErrors.NotificationTemplate.ContentNullOrEmpty);

            if (!Enum.IsDefined(typeof(NotificationType), type)) throw new DomainException(DomainErrors.NotificationTemplate.TypeNotDefined);

            return new() { Title = title, Content = content, Type = type };
        }
    }

    /// <summary>
    /// Enumeration representing the types of notifications.
    /// </summary>
    public enum NotificationType
    {
        /// <summary>
        /// Non-urgent informative content.
        /// </summary>
        Information,

        /// <summary>
        /// Informs members about actions related to transactions.
        /// </summary>
        Transaction,

        /// <summary>
        /// Informs members of planned/scheduled events, tasks, or activities.
        /// </summary>
        Reminder,

        /// <summary>
        /// Conveys critical or time-sensitive information that requires attention.
        /// </summary>
        Alert,
    }

}
