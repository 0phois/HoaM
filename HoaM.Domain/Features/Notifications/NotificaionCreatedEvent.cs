using HoaM.Domain.Common;

namespace HoaM.Domain
{
    public sealed class NotificaionCreatedEvent(Notification notification) : DomainEvent
    {
        public Notification Notification => notification;
    }
}