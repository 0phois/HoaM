using MassTransit;

namespace HoaM.Domain.Common
{
    public abstract class DomainEvent : IDomainEvent, INotifyBefore, IEquatable<DomainEvent>
    {
        public Guid Id { get; } = NewId.Next().ToGuid();

        public DateTimeOffset TriggeredOn { get; } = DateTimeOffset.UtcNow;

        public bool Equals(DomainEvent? other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as DomainEvent);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
