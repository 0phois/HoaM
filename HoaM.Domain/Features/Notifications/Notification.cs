using HoaM.Domain.Common;
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
        public required NotificationTemplate Template { get; set; }

        /// <summary>
        /// Date and time the <see cref="Notification"/> was received
        /// </summary>
        public DateTimeOffset ReceivedDate { get; private set; }

        /// <summary>
        /// Date and time the <see cref="Notification"/> was read
        /// </summary>
        public DateTimeOffset ReadDate { get; private set; } = DateTimeOffset.MinValue;

        /// <summary>
        /// Indicates whether the <see cref="Notification"/> has been viewed
        /// </summary>
        public bool Read => ReadDate != DateTimeOffset.MinValue;
    }
}
