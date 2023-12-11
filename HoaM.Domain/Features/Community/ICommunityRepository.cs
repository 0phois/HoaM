namespace HoaM.Domain
{
    public interface ICommunityRepository : IBaseRepository<Community, CommunityId>
    {
        Task<bool> IsNameUniqueAsync(CommunityName name, CancellationToken cancellationToken = default);
    }
}
