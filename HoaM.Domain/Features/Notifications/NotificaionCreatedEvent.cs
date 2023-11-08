using HoaM.Domain.Common;

namespace HoaM.Domain.Features
{
    public sealed class NotificaionCreatedEvent : DomainEvent
    {
        public Notification Notification { get; }

        public NotificaionCreatedEvent(Notification notification) => Notification = notification;
    }
}