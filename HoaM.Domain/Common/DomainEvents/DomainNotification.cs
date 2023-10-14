using MassTransit;

namespace HoaM.Domain.Common.DomainEvents
{
    public abstract class DomainNotification : IDomainEvent, INotifyAfter
    {
        public Guid Id { get; } = NewId.Next().ToGuid();

        public DateTimeOffset TriggeredOn { get; } = DateTimeOffset.UtcNow;
    }
}
