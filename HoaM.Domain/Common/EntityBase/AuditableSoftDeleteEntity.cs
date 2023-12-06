namespace HoaM.Domain.Common
{
    public abstract class AuditableSoftDeleteEntity<TId> : AuditableEntity<TId>, ISoftDelete
    {
        public AssociationMemberId? DeletedBy { get; set; }
        public DateTimeOffset? DeletionDate { get; set; }
    
        public bool IsDeleted => DeletionDate is not null;
    }
}
