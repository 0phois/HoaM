using MassTransit;
using System.ComponentModel.DataAnnotations.Schema;

namespace HoaM.Domain.Common
{
    public abstract class Entity : Entity<Guid>
    {
        protected Entity() => Id = NewId.Next().ToGuid();
    }

    public abstract class Entity<TId> : IEntity<TId>
    {
        public virtual TId Id { get; protected set; } = default!;

        private readonly List<IDomainEvent> _domainEvents = new();

        [NotMapped]
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        public bool Equals(TId? other)
        {
            return other != null && (ReferenceEquals(Id, other) || Equals(Id, other));
        }

        public bool Equals(IEntity<TId>? other)
        {
            return other != null && (ReferenceEquals(this, other) || Equals(Id, other.Id));
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as IEntity<TId>);
        }

        public override int GetHashCode()
        {
            return Id!.GetHashCode();
        }
    }
}
