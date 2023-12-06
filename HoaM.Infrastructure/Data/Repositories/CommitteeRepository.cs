using HoaM.Domain;
using HoaM.Domain.Features;
using Microsoft.EntityFrameworkCore;

namespace HoaM.Infrastructure.Data
{
    internal sealed class CommitteeRepository(DbContext dbContext) : GenericRepository<Committee, CommitteeId>(dbContext), ICommitteeRepository
    {
        public Task<bool> IsNameUniqueAsync(CommitteeName name, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
