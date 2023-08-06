using HoaM.Domain.Contracts;

namespace HoaM.Domain.Common
{
    public abstract class DomainEvent : IDomainEvent
    {
        public DateTimeOffset TriggeredOn { get; protected set; } = DateTimeOffset.UtcNow;
    }
}
