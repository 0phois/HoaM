namespace HoaM.Domain.Features
{
    public interface IMemberRepository
    {
        Task<AssociationMember> GetByIdAsync(AssociationMemberId committeeId, CancellationToken cancellationToken = default);
        
        Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default);
        
        void Insert(AssociationMember community);

        void Remove(AssociationMember community);
    }
}
