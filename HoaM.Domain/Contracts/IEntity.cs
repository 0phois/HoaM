using HoaM.Domain.Common;

namespace HoaM.Domain.Contracts
{
    public interface IEntity
    {
        IReadOnlyCollection<DomainEvent> DomainEvents { get; }

        void AddDomainEvent(DomainEvent domainEvent);
        void RemoveDomainEvent(DomainEvent domainEvent);
    }

    public interface IEntity<TId> : IEntity, IEquatable<IEntity<TId>>
    {
        TId Id { get; }
    }
}
