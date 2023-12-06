using HoaM.Domain.Common;

namespace HoaM.Domain.Features
{
    public interface IMemberRepository : IBaseRepository<AssociationMember, AssociationMemberId>
    {
        Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default);
    }
}
