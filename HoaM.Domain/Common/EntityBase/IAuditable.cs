namespace HoaM.Domain.Common
{
    /// <summary>
    /// Interface representing an auditable entity, capturing information about creation and modification.
    /// </summary>
    public interface IAuditable
    {
        /// <summary>
        /// Gets or sets the identifier of the user who created the entity.
        /// </summary>
        AssociationMemberId CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was created.
        /// </summary>
        DateTimeOffset CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified the entity.
        /// </summary>
        AssociationMemberId LastModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was last modified.
        /// If the entity has not been modified, this property may be null.
        /// </summary>
        DateTimeOffset? LastModifiedDate { get; set; }
    }

}
