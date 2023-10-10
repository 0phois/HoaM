using MassTransit;

namespace HoaM.Domain.Common.DomainEvents
{
    public abstract class DomainNotification : IDomainEvent, INotifyAfter, IEquatable<DomainNotification>
    {
        public Guid Id { get; } = NewId.Next().ToGuid();

        public DateTimeOffset TriggeredOn { get; } = DateTimeOffset.UtcNow;

        public bool Equals(DomainNotification? other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as DomainNotification);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
