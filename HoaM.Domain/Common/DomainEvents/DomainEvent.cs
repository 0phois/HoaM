using MassTransit;

namespace HoaM.Domain.Common
{
    public abstract class DomainEvent : INotifyBefore
    {
        public Guid Id { get; } = NewId.Next().ToGuid();
    }
}
