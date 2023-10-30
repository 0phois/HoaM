using MassTransit;

namespace HoaM.Domain.Common
{
    public abstract class DomainNotification : IDomainEvent, INotifyAfter
    {
        public Guid Id { get; } = NewId.Next().ToGuid();
    }
}
