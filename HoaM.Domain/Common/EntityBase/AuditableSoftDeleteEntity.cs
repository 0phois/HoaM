namespace HoaM.Domain.Common
{
    /// <summary>
    /// Base abstract class for auditable soft-deletable entities with a generic identifier.
    /// Extends the <see cref="AuditableEntity{TId}"/> class and implements the <see cref="ISoftDelete"/> interface.
    /// </summary>
    /// <typeparam name="TId">The type of the entity's identifier.</typeparam>
    public abstract class AuditableSoftDeleteEntity<TId> : AuditableEntity<TId>, ISoftDelete
    {
        /// <summary>
        /// Gets or sets the identifier of the user who performed the soft deletion.
        /// </summary>
        public AssociationMemberId? DeletedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was soft-deleted.
        /// If the entity has not been deleted, this property may be null.
        /// </summary>
        public DateTimeOffset? DeletionDate { get; set; }

        /// <summary>
        /// Gets a value indicating whether the entity has been soft-deleted.
        /// </summary>
        public bool IsDeleted => DeletionDate is not null;
    }

}
