namespace HoaM.Domain.Common
{
    public abstract class AuditableEntity<TId> : Entity<TId>, IAuditable
    {
        public AssociationMemberId CreatedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public AssociationMemberId LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedDate { get; set; }
    }
}
