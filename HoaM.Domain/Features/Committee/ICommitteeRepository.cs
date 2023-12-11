namespace HoaM.Domain
{
    public interface ICommitteeRepository : IBaseRepository<Committee, CommitteeId>
    {
        Task<bool> IsNameUniqueAsync(CommitteeName name, CancellationToken cancellationToken = default);
    }
}
