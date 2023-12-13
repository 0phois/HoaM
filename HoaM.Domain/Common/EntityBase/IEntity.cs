using System.Collections.Concurrent;

namespace HoaM.Domain.Common
{
    /// <summary>
    /// Interface representing an entity in the system.
    /// Entities are objects with distinct identities and lifecycles that encapsulate business logic and data.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets the collection of domain events associated with the entity.
        /// </summary>
        IProducerConsumerCollection<IDomainEvent> DomainEvents { get; }

        /// <summary>
        /// Adds a domain event to the entity's collection.
        /// </summary>
        /// <param name="domainEvent">The domain event to be added.</param>
        void AddDomainEvent(IDomainEvent domainEvent);

        /// <summary>
        /// Clears the collection of domain events associated with the entity.
        /// </summary>
        void ClearDomainEvents();
    }

    /// <summary>
    /// Generic interface representing an entity with a specific identifier type.
    /// Extends the <see cref="IEntity"/> interface and implements <see cref="IEquatable{T}"/> for equality comparisons.
    /// </summary>
    /// <typeparam name="TId">The type of the entity's identifier.</typeparam>
    public interface IEntity<TId> : IEntity, IEquatable<IEntity<TId>>
    {
        /// <summary>
        /// Gets the unique identifier for the entity.
        /// </summary>
        TId Id { get; }
    }

}
