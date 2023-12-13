using MassTransit;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations.Schema;

namespace HoaM.Domain.Common
{
    /// <summary>
    /// Base abstract class for entities with a <see cref="Guid"/> identifier.
    /// </summary>
    public abstract class Entity : Entity<Guid>
    {
        /// <summary>
        /// Default constructor that generates a new <see cref="Guid"/> for the entity's identifier.
        /// </summary>
        protected Entity() => Id = NewId.Next().ToGuid();
    }

    /// <summary>
    /// Base abstract class for entities with a generic identifier.
    /// Implements the <see cref="IEntity{TId}"/> interface and provides common functionality for entities.
    /// </summary>
    /// <typeparam name="TId">The type of the entity's identifier.</typeparam>
    public abstract class Entity<TId> : IEntity<TId>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the entity.
        /// </summary>
        public virtual TId Id { get; protected set; } = default!;

        /// <summary>
        /// Collection to store domain events associated with the entity.
        /// </summary>
        private readonly ConcurrentBag<IDomainEvent> _domainEvents = [];

        /// <summary>
        /// Gets the collection of domain events associated with the entity.
        /// </summary>
        [NotMapped]
        public IProducerConsumerCollection<IDomainEvent> DomainEvents => _domainEvents;

        /// <summary>
        /// Adds a domain event to the entity's collection.
        /// </summary>
        /// <param name="domainEvent">The domain event to be added.</param>
        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        /// <summary>
        /// Clears the collection of domain events associated with the entity.
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        /// <summary>
        /// Compares the entity's identifier with the specified identifier for equality.
        /// </summary>
        /// <param name="other">The identifier to compare with.</param>
        /// <returns>True if the identifiers are equal, false otherwise.</returns>
        public virtual bool Equals(TId? other)
        {
            return other != null && (ReferenceEquals(Id, other) || Equals(Id, other));
        }

        /// <summary>
        /// Compares the entity with another entity for equality based on their identifiers.
        /// </summary>
        /// <param name="other">The entity to compare with.</param>
        /// <returns>True if the entities are equal, false otherwise.</returns>
        public virtual bool Equals(IEntity<TId>? other)
        {
            return other != null && (ReferenceEquals(this, other) || Equals(Id, other.Id));
        }

        /// <summary>
        /// Compares the entity with an object for equality based on their identifiers.
        /// </summary>
        /// <param name="obj">The object to compare with.</param>
        /// <returns>True if the object is an entity and they are equal, false otherwise.</returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as IEntity<TId>);
        }

        /// <summary>
        /// Computes the hash code for the entity based on its identifier.
        /// </summary>
        /// <returns>The hash code for the entity.</returns>
        public override int GetHashCode()
        {
            return Id!.GetHashCode();
        }
    }

}
