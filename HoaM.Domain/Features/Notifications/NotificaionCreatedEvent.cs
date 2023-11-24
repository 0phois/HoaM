using HoaM.Domain.Common;

namespace HoaM.Domain.Features
{
    public sealed class NotificaionCreatedEvent(Notification notification) : DomainEvent
    {
        public Notification Notification => notification;
    }
}