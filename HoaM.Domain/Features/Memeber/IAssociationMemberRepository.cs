namespace HoaM.Domain
{
    public interface IAssociationMemberRepository : IBaseRepository<AssociationMember, AssociationMemberId>
    {
        Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default);
    }
}
