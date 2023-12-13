namespace HoaM.Domain.Common
{
    /// <summary>
    /// Base abstract class for auditable entities with a generic identifier.
    /// Extends the <see cref="Entity{TId}"/> class and implements the <see cref="IAuditable"/> interface.
    /// </summary>
    /// <typeparam name="TId">The type of the entity's identifier.</typeparam>
    public abstract class AuditableEntity<TId> : Entity<TId>, IAuditable
    {
        /// <summary>
        /// Gets or sets the identifier of the user who created the entity.
        /// </summary>
        public AssociationMemberId CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was created.
        /// </summary>
        public DateTimeOffset CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified the entity.
        /// </summary>
        public AssociationMemberId LastModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was last modified.
        /// If the entity has not been modified, this property may be null.
        /// </summary>
        public DateTimeOffset? LastModifiedDate { get; set; }
    }

}
