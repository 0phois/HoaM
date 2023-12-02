using HoaM.Domain;
using HoaM.Domain.Features;
using Microsoft.EntityFrameworkCore;

namespace HoaM.Infrastructure.Data
{
    internal sealed class CommunityRepository(DbContext dbContext) : GenericRepository<Community, CommunityId>(dbContext), ICommunityRepository
    {
        public Task<Community> GetByIdAsync(CommunityId communityId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsNameUniqueAsync(CommunityName name, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
