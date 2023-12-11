using HoaM.Domain;
using HoaM.Domain.Features;
using Microsoft.EntityFrameworkCore;

namespace HoaM.Infrastructure.Data
{
    internal sealed class AssociationMemberRepository(DbContext dbContext) : GenericRepository<AssociationMember, AssociationMemberId>(dbContext), IAssociationMemberRepository
    {
        public Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
