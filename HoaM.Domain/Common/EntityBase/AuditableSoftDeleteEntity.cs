namespace HoaM.Domain.Common
{
    public abstract class AuditableSoftDeleteEntity<TId> : AuditableEntity<TId>, ISoftDelete
    {
        public bool IsDeleted { get; }
        public AssociationMemberId? DeletedBy { get; set; }
        public DateTimeOffset? DeletionDate { get; set; }
    }
}
