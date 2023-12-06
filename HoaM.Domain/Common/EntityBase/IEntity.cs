using System.Collections.Concurrent;

namespace HoaM.Domain.Common
{
    public interface IEntity
    {
        IProducerConsumerCollection<IDomainEvent> DomainEvents { get; }

        void AddDomainEvent(IDomainEvent domainEvent);

        void ClearDomainEvents();
    }

    public interface IEntity<TId> : IEntity, IEquatable<IEntity<TId>>
    {
        TId Id { get; }
    }
}
