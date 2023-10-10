namespace HoaM.Domain.Common
{
    public interface ISoftDelete
    {
        /// <summary>
        /// Defines whether the entity is deleted
        /// </summary>
        bool IsDeleted => DeletionDate is not null;

        /// <summary>
        /// Id of the <see cref="AssociationMember"/> that deleted the entity
        /// </summary>
        AssociationMemberId? DeletedBy { get; set; }

        /// <summary>
        /// Date and time the entity was deleted
        /// </summary>
        DateTimeOffset? DeletionDate { get; set; }

        void UndoDelete()
        {
            DeletedBy = null;
            DeletionDate = null;
        }
    }
}
