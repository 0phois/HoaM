namespace HoaM.Domain.Common
{
    /// <summary>
    /// Interface representing a soft-deletable entity, allowing logical deletion without immediate removal from the system.
    /// </summary>
    public interface ISoftDelete
    {
        /// <summary>
        /// Gets a value indicating whether the entity has been soft-deleted.
        /// </summary>
        bool IsDeleted => DeletionDate is not null;

        /// <summary>
        /// Gets or sets the identifier of the user who performed the soft deletion.
        /// </summary>
        AssociationMemberId? DeletedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was soft-deleted.
        /// If the entity has not been deleted, this property may be null.
        /// </summary>
        DateTimeOffset? DeletionDate { get; set; }

        /// <summary>
        /// Restores the entity from a soft-deleted state by resetting deletion-related properties.
        /// </summary>
        void UndoDelete()
        {
            DeletedBy = null;
            DeletionDate = null;
        }
    }

}
