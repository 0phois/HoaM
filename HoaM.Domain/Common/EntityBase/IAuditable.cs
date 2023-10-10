namespace HoaM.Domain.Common
{
    public interface IAuditable
    {
        AssociationMemberId CreatedBy { get; set; }
        DateTimeOffset CreatedDate { get; set; }

        AssociationMemberId LastModifiedBy { get; set; }
        DateTimeOffset? LastModifiedDate { get; set; }
    }
}
