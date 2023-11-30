namespace HoaM.Domain.Features
{
    public interface ICommitteeRepository
    {
        Task<Committee> GetByIdAsync(CommitteeId committeeId, CancellationToken cancellationToken = default);

        Task<bool> IsNameUniqueAsync(CommitteeName name, CancellationToken cancellationToken = default);
    
        void Insert(Committee community);

        void Remove(Committee community);
    }
}
