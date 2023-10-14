﻿using HoaM.Domain.Common;
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
        public bool Read => ReadDate != DateTimeOffset.MinValue;

        private Notification() { }

        public static Notification Create(NotificationTemplate template) 
        {
            return new() { Template = template };
        }

        public IResult Publish(AssociationMember member, INotificationManager notificationManager)
        {
            if (notificationManager is null) throw new ArgumentNullException(nameof(notificationManager), "Value cannot be null.");

            ReceivedDate = notificationManager.SystemClock.UtcNow;

            var deliveryResult = notificationManager.DeliverTo(member);
                
            if (deliveryResult.IsSuccess) Template.DeliveredTo.Add(member);

            return deliveryResult;
        }

        public void MarkAsRead(INotificationManager notificationManager)
        {
            if (notificationManager is null) throw new ArgumentNullException(nameof(notificationManager), "Value cannot be null.");

            ReadDate = notificationManager.SystemClock.UtcNow;
        }

        public void MarkAsUnread() => ReadDate = DateTimeOffset.MinValue;
    }
}
