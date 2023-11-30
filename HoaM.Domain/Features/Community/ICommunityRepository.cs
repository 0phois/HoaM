namespace HoaM.Domain.Features
{
    public interface ICommunityRepository
    {
        Task<Community> GetByIdAsync(CommunityId communityId, CancellationToken cancellationToken = default);

        Task<bool> IsNameUniqueAsync(CommunityName name, CancellationToken cancellationToken = default);

        void Insert(Community community);

        void Remove(Community community);
    }
}
