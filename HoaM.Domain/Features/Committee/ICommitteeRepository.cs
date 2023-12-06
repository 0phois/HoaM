using HoaM.Domain.Common;

namespace HoaM.Domain.Features
{
    public interface ICommitteeRepository : IBaseRepository<Committee, CommitteeId>
    {
        Task<bool> IsNameUniqueAsync(CommitteeName name, CancellationToken cancellationToken = default);
    }
}
