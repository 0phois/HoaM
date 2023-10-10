namespace HoaM.Domain.Common
{
    public interface IEntity
    {
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

        void AddDomainEvent(IDomainEvent domainEvent);
        void RemoveDomainEvent(IDomainEvent domainEvent);
    }

    public interface IEntity<TId> : IEntity, IEquatable<IEntity<TId>>
    {
        TId Id { get; }
    }
}
